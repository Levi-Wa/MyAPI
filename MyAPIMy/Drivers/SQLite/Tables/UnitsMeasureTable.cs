using Microsoft.Data.Sqlite;
using MyAPP.Common.DB;

namespace MyAPP.Drivers.SQLite.Tables;

public class UnitsMeasureTable : ITableInitializer
{
    public TableOrder Id => TableOrder.UnitsMeasure;
    public int Order => TableCreationOrder.UnitsMeasure;
    public string TableName => "units_measure";

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

    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS units_measure (
                unit_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                symbol TEXT NOT NULL
            );
            """;

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
