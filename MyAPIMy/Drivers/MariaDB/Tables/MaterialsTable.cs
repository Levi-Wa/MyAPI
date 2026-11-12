using MySqlConnector;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.MariaDB.Tables;

public class MaterialsTable : ITableInitializer
{
    public TableOrder Id => TableOrder.Materials;
    public int Order => TableCreationOrder.Materials;
    public string TableName => "materials";

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS materials (
                catalog_id INT AUTO_INCREMENT PRIMARY KEY,
                catalog_number VARCHAR(20) NOT NULL,
                name VARCHAR(100) NOT NULL,
                grade VARCHAR(50) NOT NULL,
                profile_size VARCHAR(50) NOT NULL,
                unit_id INT NOT NULL,
                stock_norm DECIMAL(14, 4) NOT NULL DEFAULT 0.0000
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public async Task InitializeAsync(MySqlConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS materials (
                catalog_id INT AUTO_INCREMENT PRIMARY KEY,
                catalog_number VARCHAR(20) NOT NULL,
                name VARCHAR(100) NOT NULL,
                grade VARCHAR(50) NOT NULL,
                profile_size VARCHAR(50) NOT NULL,
                unit_id INT NOT NULL,
                stock_norm DECIMAL(14, 4) NOT NULL DEFAULT 0.0000
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
