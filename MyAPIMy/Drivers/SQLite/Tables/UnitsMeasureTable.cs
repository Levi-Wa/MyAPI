using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class UnitsMeasureTable : ITableInitializer
{
    public int Order => 1;
    public string TableName => "units_measure";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS units_measure (
                unit_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                symbol TEXT NOT NULL
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
