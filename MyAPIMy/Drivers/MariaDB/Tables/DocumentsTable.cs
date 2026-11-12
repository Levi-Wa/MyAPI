using MySqlConnector;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.MariaDB.Tables;

public class DocumentsTable : ITableInitializer
{
    public TableOrder Id => TableOrder.Documents;
    public int Order => TableCreationOrder.Documents;
    public string TableName => "documents";

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

    public async Task InitializeAsync(MySqlConnection connection, CancellationToken cancellationToken = default)
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

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
