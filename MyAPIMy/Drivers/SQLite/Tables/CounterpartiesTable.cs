using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class CounterpartiesTable : ITableInitializer
{
    public int Order => 2;
    public string TableName => "counterparties";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS counterparties (
                counterparty_id  INTEGER PRIMARY KEY AUTOINCREMENT,
                name             TEXT    NOT NULL,
                type             TEXT    NOT NULL
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
