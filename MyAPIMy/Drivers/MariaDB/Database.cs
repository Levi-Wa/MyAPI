using MySqlConnector;
using MyAPP.Common;
using MyAPP.Common.DB.IDBUnits;
using MyAPP.Drivers.MariaDB.Tables;

namespace MyAPP.Drivers.MariaDB;

public class Database : IDatabase
{
    private readonly string? _connectionString;
    private readonly DatabaseInitializer _initializer;

    public ICounterparties Counterparties { get; set; } = null!;
    public IDocuments Documents { get; set; } = null!;
    public IMaterialMovements MaterialMovements { get; set; } = null!;
    public IMaterials Materials { get; set; } = null!;
    public IUnitsMeasure UnitsMeasure { get; set; } = null!;

    public Database(string? connectionString = null, DatabaseInitializer? initializer = null)
    {
        _connectionString = connectionString;
        _initializer = initializer ?? new DatabaseInitializer();
    }

    public void Initialize()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Connection string is not configured.");
        }

        using var connection = new MySqlConnection(_connectionString);
        _initializer.Initialize(connection);
    }

    public void Initialize(MySqlConnection connection)
    {
        _initializer.Initialize(connection);
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Connection string is not configured.");
        }

        await using var connection = new MySqlConnection(_connectionString);
        await _initializer.InitializeAsync(connection, cancellationToken);
    }

    public async Task InitializeAsync(MySqlConnection connection, CancellationToken cancellationToken = default)
    {
        await _initializer.InitializeAsync(connection, cancellationToken);
    }
}
