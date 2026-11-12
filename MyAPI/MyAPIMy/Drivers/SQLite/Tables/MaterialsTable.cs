using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class MaterialsTable : ITableInitializer
{
    public int Order => 3;
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
                stock_norm NUMERIC NOT NULL DEFAULT 0,
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
