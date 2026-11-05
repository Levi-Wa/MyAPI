using MySqlConnector;

namespace MyAPP.Drivers.MariaDB.Tables;

public class UnitsMeasureTable : ITableInitializer
{
    public int Order => 1;
    public string TableName => "units_measure";

    public void Initialize(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS units_measure (
                unit_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                name VARCHAR(50) NOT NULL,
                symbol VARCHAR(10) NOT NULL
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
