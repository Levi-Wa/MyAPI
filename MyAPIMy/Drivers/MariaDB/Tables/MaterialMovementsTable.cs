using MySqlConnector;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.MariaDB.Tables;

public class MaterialMovementsTable : ITableInitializer
{
    public TableOrder Id => TableOrder.MaterialMovements;
    public int Order => TableCreationOrder.MaterialMovements;
    public string TableName => "material_movements";

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS material_movements (
                record_id INT AUTO_INCREMENT PRIMARY KEY,
                document_id INT NOT NULL,
                catalog_id INT NOT NULL,
                document_price DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
                income DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
                expense DECIMAL(14, 4) NOT NULL DEFAULT 0.0000
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public async Task InitializeAsync(MySqlConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS material_movements (
                record_id INT AUTO_INCREMENT PRIMARY KEY,
                document_id INT NOT NULL,
                catalog_id INT NOT NULL,
                document_price DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
                income DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
                expense DECIMAL(14, 4) NOT NULL DEFAULT 0.0000
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
