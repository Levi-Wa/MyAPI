using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

public class MaterialsTable : ITableInitializer
{
    public TableOrder Id => TableOrder.Materials;
    public int Order => TableCreationOrder.Materials;
    public string TableName => "materials";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS materials (
                catalog_id INTEGER PRIMARY KEY AUTOINCREMENT,
                catalog_number TEXT NOT NULL,
                name TEXT NOT NULL,
                grade TEXT NOT NULL,
                profile_size TEXT NOT NULL,
                unit_id INTEGER NOT NULL,
                stock_norm NUMERIC NOT NULL DEFAULT 0
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS materials (
                catalog_id INTEGER PRIMARY KEY AUTOINCREMENT,
                catalog_number TEXT NOT NULL,
                name TEXT NOT NULL,
                grade TEXT NOT NULL,
                profile_size TEXT NOT NULL,
                unit_id INTEGER NOT NULL,
                stock_norm NUMERIC NOT NULL DEFAULT 0
            );
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
