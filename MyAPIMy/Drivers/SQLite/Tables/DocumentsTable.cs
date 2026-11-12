using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

public class DocumentsTable : ITableInitializer
{
    public TableOrder Id => TableOrder.Documents;
    public int Order => TableCreationOrder.Documents;
    public string TableName => "documents";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS documents (
                document_id INTEGER PRIMARY KEY AUTOINCREMENT,
                document_number TEXT NOT NULL,
                record_date TEXT NOT NULL DEFAULT (datetime('now')),
                counterparty_id INTEGER NOT NULL,
                document_type INTEGER NOT NULL
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS documents (
                document_id INTEGER PRIMARY KEY AUTOINCREMENT,
                document_number TEXT NOT NULL,
                record_date TEXT NOT NULL DEFAULT (datetime('now')),
                counterparty_id INTEGER NOT NULL,
                document_type INTEGER NOT NULL
            );
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
