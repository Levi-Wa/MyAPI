using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class DocumentsTable : ITableInitializer
{
    public int Order => 4;
    public string TableName => "documents";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS documents (
                document_id INTEGER PRIMARY KEY AUTOINCREMENT,
                document_number TEXT NOT NULL,
                record_date TEXT NOT NULL DEFAULT (datetime('now')),
                counterparty_id INTEGER NOT NULL,
                document_type INTEGER NOT NULL,
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
