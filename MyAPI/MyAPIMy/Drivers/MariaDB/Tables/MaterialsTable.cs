using MySqlConnector;

namespace MyAPP.Drivers.MariaDB.Tables;

public class MaterialsTable : ITableInitializer
{
    public int Order => 3;
    public string TableName => "materials";

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS materials (
                catalog_id INTAUTO_INCREMENT PRIMARY KEY,
                catalog_number VARCHAR(20) NOT NULL,
                name VARCHAR(100) NOT NULL,
                grade VARCHAR(50) NOT NULL,
                profile_size VARCHAR(50) NOT NULL,
                unit_id INT NOT NULL,
                stock_norm DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
