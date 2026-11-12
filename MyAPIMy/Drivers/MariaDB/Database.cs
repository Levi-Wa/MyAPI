using MySqlConnector;
using MyAPP.Common;
using MyAPP.Common.DB.IDBUnits;
using MyAPP.Drivers.MariaDB.Tables;

namespace MyAPP.Drivers.MariaDB;

public class Database : IDatabase
{
    private readonly string? _connectionString;
    private readonly DatabaseInitializer? _initializer;

    public ICounterparties Counterparties { get; set; } = null!;
    public IDocuments Documents { get; set; } = null!;
    public IMaterialMovements MaterialMovements { get; set; } = null!;
    public IMaterials Materials { get; set; } = null!;
    public IUnitsMeasure UnitsMeasure { get; set; } = null!;

    private static readonly Dictionary<TableId, Action<MySqlConnection>> creators = new()
    {
        [TableId.UnitsMeasureTable] = UnitsMeasureTable.EnsureTableCreated,
        [TableId.CounterpartiesTable] = CounterpartiesTable.EnsureTableCreated,
        [TableId.MaterialsTable] = MaterialsTable.EnsureTableCreated,
        [TableId.DocumentsTable] = DocumentsTable.EnsureTableCreated,
        [TableId.MaterialMovementsTable] = MaterialMovementsTable.EnsureTableCreated,
    };

    public Database(string? connectionString = null, DatabaseInitializer? initializer = null)
    {
        _connectionString = connectionString;
        _initializer = initializer;
    }

    public void Initialize()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Connection string is not configured.");
        }

        // сначала пройти по TableOrder.Creation и проверить, что для каждого TableId есть запись в creators;
        // если нет, выбросить InvalidOperationException ещё до создания первой таблицы;
        foreach (var id in TableOrder.Creation)
        {
            if (!creators.ContainsKey(id))
            {
                throw new InvalidOperationException($"Отсутствует создатель для таблицы {id}.");
            }
        }

        // открыть одно подключение;
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        // пройти обычным синхронным foreach по TableOrder.Creation и вызвать creators[id](connection) строго по очереди;
        // Для MariaDB транзакцию не использовать (CREATE TABLE завершает её автоматически);
        // повторный запуск должен безопасно доделывать недостающие таблицы.
        foreach (var id in TableOrder.Creation)
        {
            try
            {
                // каждый вызов обернуть в try/catch и при ошибке выбросить InvalidOperationException
                // с названием таблицы ($"Не удалось создать таблицу {id}") и исходным исключением внутри;
                // на первой ошибке остановиться и дальше не создавать.
                creators[id](connection);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Не удалось создать таблицу {id}", ex);
            }
        }
    }

    public void Initialize(MySqlConnection connection)
    {
        foreach (var id in TableOrder.Creation)
        {
            if (!creators.ContainsKey(id))
            {
                throw new InvalidOperationException($"Отсутствует создатель для таблицы {id}.");
            }
        }

        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        foreach (var id in TableOrder.Creation)
        {
            try
            {
                creators[id](connection);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Не удалось создать таблицу {id}", ex);
            }
        }
    }
}
