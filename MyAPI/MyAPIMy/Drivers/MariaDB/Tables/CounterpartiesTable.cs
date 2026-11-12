using MySqlConnector;

namespace MyAPP.Drivers.MariaDB.Tables;

public class CounterpartiesTable : ITableInitializer
{
    public int Order => 2;
    public string TableName => "counterparties";

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS counterparties (
                counterparty_id INT AUTO_INCREMENT PRIMARY KEY,
                name VARCHAR(100) NOT NULL,
                type VARCHAR(50) NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; 
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
