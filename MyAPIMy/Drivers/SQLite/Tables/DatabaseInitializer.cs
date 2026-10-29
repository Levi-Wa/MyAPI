using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class DatabaseInitializer
{
    private readonly IReadOnlyList<ITableInitializer> _tableInitializers;

    public DatabaseInitializer(IEnumerable<ITableInitializer>? tableInitializers = null)
    {
        _tableInitializers = (tableInitializers ?? [
            new UnitsMeasureTable(),
            new CounterpartiesTable(),
            new MaterialsTable(),
            new DocumentsTable(),
            new MaterialMovementsTable()
        ])
        .OrderBy(t => t.Order)
        .ToList();
    }

    public void Initialize(SqliteConnection connection)
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        using (var pragmaCmd = connection.CreateCommand())
        {
            pragmaCmd.CommandText = "PRAGMA foreign_keys = ON;";
            pragmaCmd.ExecuteNonQuery();
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            foreach (var tableInitializer in _tableInitializers)
            {
                tableInitializer.Initialize(connection);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
