using Microsoft.Data.Sqlite;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class CounterpartiesTable : ITableInitializer
{
    public TableId Id => TableId.CounterpartiesTable;
    public string TableName => "counterparties";

    public static void EnsureTableCreated(SqliteConnection connection) => new CounterpartiesTable().Initialize(connection);

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS counterparties (
                counterparty_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                type TEXT NOT NULL
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
