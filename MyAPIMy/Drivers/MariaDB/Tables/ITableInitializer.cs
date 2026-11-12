using MySqlConnector;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.MariaDB.Tables;

public interface ITableInitializer
{
    TableOrder Id { get; }
    int Order { get; }
    string TableName { get; }
    void Initialize(MySqlConnection connection);
    Task InitializeAsync(MySqlConnection connection, CancellationToken cancellationToken = default);
}
