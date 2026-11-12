using MySqlConnector;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class UnitsMeasureTable : ITableInitializer
{
    public TableId Id => TableId.UnitsMeasureTable;
    public string TableName => "units_measure";

    public static void EnsureTableCreated(MySqlConnection connection) => new UnitsMeasureTable().Initialize(connection);

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS units_measure (
                unit_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                name VARCHAR(50) NOT NULL,
                symbol VARCHAR(10) NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
