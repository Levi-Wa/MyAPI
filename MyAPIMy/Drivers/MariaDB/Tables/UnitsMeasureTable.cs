using MySqlConnector;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.MariaDB.Tables;

public class UnitsMeasureTable : ITableInitializer
{
    public TableOrder Id => TableOrder.UnitsMeasure;
    public int Order => TableCreationOrder.UnitsMeasure;
    public string TableName => "units_measure";

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

    public async Task InitializeAsync(MySqlConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS units_measure (
                unit_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                name VARCHAR(50) NOT NULL,
                symbol VARCHAR(10) NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
