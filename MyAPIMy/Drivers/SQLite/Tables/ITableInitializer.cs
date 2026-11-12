using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

public interface ITableInitializer
{
    TableOrder Id { get; }
    int Order { get; }
    string TableName { get; }
    void Initialize(SqliteConnection connection);
    Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default);
}
