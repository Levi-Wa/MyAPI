using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

// Дескриптор инициализатора таблицы базы данных с идентификацией.
public readonly record struct TableStep(TableOrder Id, string TableName, int Order, ITableInitializer Initializer);

public class DatabaseInitializer
{
    // Строго последовательный упорядоченный массив шагов создания таблиц.
    // Фиксированный массив гарантирует детерминированный порядок выполнения (друг за другом).

    private readonly TableStep[] _pipeline;

    public IReadOnlyList<TableStep> Pipeline => _pipeline;

    public DatabaseInitializer(IEnumerable<ITableInitializer>? tableInitializers = null)
    {
        var initializers = tableInitializers ?? [
            new UnitsMeasureTable(),
            new CounterpartiesTable(),
            new MaterialsTable(),
            new DocumentsTable(),
            new MaterialMovementsTable()
        ];

        // Записываем в коллекцию-массив с идентификатором, строго сортируя по очередности
        _pipeline = initializers
            .Select(t => new TableStep(t.Id, t.TableName, t.Order, t))
            .OrderBy(step => step.Order)
            .ToArray();
    }

    // Синхронное последовательное создание таблиц шаг за шагом.
    public void Initialize(SqliteConnection connection)
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        using (var pragmaCmd = connection.CreateCommand())
        {
            pragmaCmd.CommandText = "PRAGMA foreign_keys = ON;";
            pragmaCmd.ExecuteNonQuery();
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            // Строго последовательный запуск: следующая таблица создаётся только после завершения предыдущей
            for (int i = 0; i < _pipeline.Length; i++)
            {
                _pipeline[i].Initializer.Initialize(connection);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    // Асинхронное последовательное создание таблиц шаг за шагом.
    // Гарантирует отсутствие параллельного выполнения и сохранение порядка 1 -> 2 -> 3 -> 4 -> 
    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default)
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        await using (var pragmaCmd = connection.CreateCommand())
        {
            pragmaCmd.CommandText = "PRAGMA foreign_keys = ON;";
            await pragmaCmd.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        try
        {
            // Строго последовательный await в цикле (не параллельный Task.WhenAll)
            for (int i = 0; i < _pipeline.Length; i++)
            {
                await _pipeline[i].Initializer.InitializeAsync(connection, cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
