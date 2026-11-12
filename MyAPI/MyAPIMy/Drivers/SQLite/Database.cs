using Microsoft.Data.Sqlite;
using MyAPP.Common;
using MyAPP.Common.DB.IDBUnits;
using MyAPP.Drivers.SQLite.Tables;

namespace MyAPP.Drivers.SQLite;

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

        using var connection = new SqliteConnection(_connectionString);
        _initializer.Initialize(connection);
    }

    public void Initialize(SqliteConnection connection)
    {
        _initializer.Initialize(connection);
    }
}

