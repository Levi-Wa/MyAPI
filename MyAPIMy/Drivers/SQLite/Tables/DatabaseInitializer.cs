using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class DatabaseInitializer
{
    public void Initialize(SqliteConnection connection)
    {
        var db = new Database();
        db.Initialize(connection);
    }
}
