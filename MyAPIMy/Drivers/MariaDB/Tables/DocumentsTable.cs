using MySqlConnector;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class DocumentsTable : ITableInitializer
{
    public TableId Id => TableId.DocumentsTable;
    public string TableName => "documents";

    public static void EnsureTableCreated(MySqlConnection connection) => new DocumentsTable().Initialize(connection);

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS documents (
                document_id INT AUTO_INCREMENT PRIMARY KEY,
                document_number VARCHAR(50) NOT NULL,
                record_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                counterparty_id INT UNSIGNED NOT NULL,
                document_type INT NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
