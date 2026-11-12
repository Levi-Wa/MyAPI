using Microsoft.Data.Sqlite;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class DocumentsTable : ITableInitializer
{
    public TableId Id => TableId.DocumentsTable;
    public string TableName => "documents";

    public static void EnsureTableCreated(SqliteConnection connection) => new DocumentsTable().Initialize(connection);

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
}
