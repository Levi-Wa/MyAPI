using MySqlConnector;

namespace MyAPP.Drivers.MariaDB.Tables;

public class DatabaseInitializer
{
    public void Initialize(MySqlConnection connection)
    {
        var db = new Database();
        db.Initialize(connection);
    }
}
