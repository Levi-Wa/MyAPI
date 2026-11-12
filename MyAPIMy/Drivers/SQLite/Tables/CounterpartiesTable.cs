using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

public class CounterpartiesTable : ITableInitializer
{
    public TableOrder Id => TableOrder.Counterparties;
    public int Order => TableCreationOrder.Counterparties;
    public string TableName => "counterparties";

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

    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS counterparties (
                counterparty_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                type TEXT NOT NULL
            );
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
