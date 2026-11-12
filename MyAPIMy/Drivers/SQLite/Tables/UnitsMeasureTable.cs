using Microsoft.Data.Sqlite;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class UnitsMeasureTable : ITableInitializer
{
    public TableId Id => TableId.UnitsMeasureTable;
    public string TableName => "units_measure";

    public static void EnsureTableCreated(SqliteConnection connection) => new UnitsMeasureTable().Initialize(connection);

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS units_measure (
                unit_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                symbol TEXT NOT NULL
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
