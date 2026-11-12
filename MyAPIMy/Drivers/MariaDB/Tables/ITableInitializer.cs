using MySqlConnector;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public interface ITableInitializer
{
    TableId Id { get; }
    string TableName { get; }
    void Initialize(MySqlConnection connection);
}
