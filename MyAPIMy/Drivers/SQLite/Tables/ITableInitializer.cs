using Microsoft.Data.Sqlite;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public interface ITableInitializer
{
    TableId Id { get; }
    string TableName { get; }
    void Initialize(SqliteConnection connection);
}
