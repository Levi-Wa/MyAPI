using MySqlConnector;

namespace MyAPP.Drivers.MariaDB.Tables;

public interface ITableInitializer
{
    int Order { get; }
    string TableName { get; }
    void Initialize(MySqlConnection connection);
}
