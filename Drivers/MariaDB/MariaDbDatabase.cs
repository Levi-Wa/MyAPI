using System.Data;
using MySqlConnector;
using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;
using MyAPP.Drivers.MariaDB.Tables;

namespace MyAPP.Drivers.MariaDB;

/// <summary>
/// Контекст базы данных MariaDB. Хранит эталонное соединение для клонирования.
/// </summary>
public class MariaDbDatabase : IDatabase
{
    private readonly MySqlConnection _prototypeConnection;
    private int _released;
    private int _activeOperations;

    public IUnitsMeasure UnitsMeasure { get; }
    public ICounterparties Counterparties { get; }
    public IMaterials Materials { get; }
    public IDocuments Documents { get; }
    public IMaterialMovements MaterialMovements { get; }

    public MariaDbDatabase(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения MariaDB не может быть пустой.", nameof(connectionString));

        _prototypeConnection = new MySqlConnection(connectionString);

        UnitsMeasure = new UnitsMeasureTable(new MariaDbExecutor<UnitsMeasure>(_prototypeConnection, BeginOperation, EndOperation));
        Counterparties = new CounterpartiesTable(new MariaDbExecutor<Counterparties>(_prototypeConnection, BeginOperation, EndOperation));
        Materials = new MaterialsTable(new MariaDbExecutor<Materials>(_prototypeConnection, BeginOperation, EndOperation));
        Documents = new DocumentsTable(new MariaDbExecutor<Documents>(_prototypeConnection, BeginOperation, EndOperation));
        MaterialMovements = new MaterialMovementsTable(new MariaDbExecutor<MaterialMovements>(_prototypeConnection, BeginOperation, EndOperation));
    }

    public bool TestConnection()
    {
        BeginOperation();
        try
        {
            using var connection = _prototypeConnection.Clone();
            connection.Open();
            return connection.State == ConnectionState.Open;
        }
        catch
        {
            return false;
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
            using var connection = _prototypeConnection.Clone();
            connection.Open();

            foreach (var sql in createScripts)
            {
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
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
            throw new ObjectDisposedException(nameof(MariaDbDatabase));
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
        _prototypeConnection.Dispose();
    }

    public void Dispose()
    {
        Release();
    }
}
