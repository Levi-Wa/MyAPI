using MySqlConnector;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class MaterialMovementsTable : ITableInitializer
{
    public TableId Id => TableId.MaterialMovementsTable;
    public string TableName => "material_movements";

    public static void EnsureTableCreated(MySqlConnection connection) => new MaterialMovementsTable().Initialize(connection);

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
}
