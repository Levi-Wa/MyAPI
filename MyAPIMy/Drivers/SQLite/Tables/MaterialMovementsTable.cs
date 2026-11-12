using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

public class MaterialMovementsTable : ITableInitializer
{
    public TableOrder Id => TableOrder.MaterialMovements;
    public int Order => TableCreationOrder.MaterialMovements;
    public string TableName => "material_movements";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS material_movements (
                record_id INTEGER PRIMARY KEY AUTOINCREMENT,
                document_id INTEGER NOT NULL,
                catalog_id INTEGER NOT NULL,
                document_price NUMERIC NOT NULL DEFAULT 0,
                income NUMERIC NOT NULL DEFAULT 0,
                expense NUMERIC NOT NULL DEFAULT 0
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS material_movements (
                record_id INTEGER PRIMARY KEY AUTOINCREMENT,
                document_id INTEGER NOT NULL,
                catalog_id INTEGER NOT NULL,
                document_price NUMERIC NOT NULL DEFAULT 0,
                income NUMERIC NOT NULL DEFAULT 0,
                expense NUMERIC NOT NULL DEFAULT 0
            );
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
