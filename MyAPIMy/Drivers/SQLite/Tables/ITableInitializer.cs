using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public interface ITableInitializer
{
    int Order { get; }
    string TableName { get; }
    void Initialize(SqliteConnection connection);
}
