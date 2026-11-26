using System.Data;
using Microsoft.Data.Sqlite;
using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;
using MyAPP.Drivers.SQLite.Tables;

namespace MyAPP.Drivers.SQLite;

/// <summary>
/// Контекст базы данных SQLite. Хранит строку подключения и единый Mutex для всех операций.
/// </summary>
public class SqliteDatabase : IDatabase
{
    private readonly string _connectionString;
    private readonly Mutex _mutex = new(false);
    private int _released;
    private int _activeOperations;

    public IUnitsMeasure UnitsMeasure { get; }
    public ICounterparties Counterparties { get; }
    public IMaterials Materials { get; }
    public IDocuments Documents { get; }
    public IMaterialMovements MaterialMovements { get; }

    public SqliteDatabase(string connectionString = "Data Source=warehouse.db")
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения SQLite не может быть пустой.", nameof(connectionString));

        _connectionString = connectionString;

        UnitsMeasure = new UnitsMeasureTable(new SqliteExecutor<UnitsMeasure>(_connectionString, _mutex, BeginOperation, EndOperation));
        Counterparties = new CounterpartiesTable(new SqliteExecutor<Counterparties>(_connectionString, _mutex, BeginOperation, EndOperation));
        Materials = new MaterialsTable(new SqliteExecutor<Materials>(_connectionString, _mutex, BeginOperation, EndOperation));
        Documents = new DocumentsTable(new SqliteExecutor<Documents>(_connectionString, _mutex, BeginOperation, EndOperation));
        MaterialMovements = new MaterialMovementsTable(new SqliteExecutor<MaterialMovements>(_connectionString, _mutex, BeginOperation, EndOperation));
    }

    public bool TestConnection()
    {
        BeginOperation();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                return connection.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            EndOperation();
        }
    }

    public void Initialize()
    {
        // Порядок создания учитывает зависимости внешних ключей
        string[] createScripts =
        [
            UnitsMeasureTable.CreateSql,
            CounterpartiesTable.CreateSql,
            MaterialsTable.CreateSql,
            DocumentsTable.CreateSql,
            MaterialMovementsTable.CreateSql
        ];

        BeginOperation();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                using (var pragma = connection.CreateCommand())
                {
                    pragma.CommandText = "PRAGMA foreign_keys = ON; PRAGMA journal_mode = WAL; PRAGMA busy_timeout = 5000;";
                    pragma.ExecuteNonQuery();
                }

                foreach (var sql in createScripts)
                {
                    using var command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            EndOperation();
        }
    }

    internal void BeginOperation()
    {
        Interlocked.Increment(ref _activeOperations);
        if (Volatile.Read(ref _released) != 0)
        {
            Interlocked.Decrement(ref _activeOperations);
            throw new ObjectDisposedException(nameof(SqliteDatabase));
        }
    }

    internal void EndOperation()
    {
        Interlocked.Decrement(ref _activeOperations);
    }

    public void Release()
    {
        if (Interlocked.Exchange(ref _released, 1) != 0)
            return;

        SpinWait.SpinUntil(() => Volatile.Read(ref _activeOperations) == 0);
        _mutex.Dispose();
    }

    public void Dispose()
    {
        Release();
    }
}
