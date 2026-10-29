using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

public class MaterialsTable : ITableInitializer
{
    public int Order => 3;
    public string TableName => "materials";

    public void Initialize(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS materials (
                catalog_id INTEGER PRIMARY KEY AUTOINCREMENT,
                catalog_number TEXT NOT NULL,
                name TEXT NOT NULL,
                grade TEXT NOT NULL,
                profile_size TEXT NOT NULL,
                unit_id INTEGER NOT NULL,
                stock_norm NUMERIC NOT NULL DEFAULT 0,

                CONSTRAINT chk_material_stock_norm
                    CHECK (stock_norm >= 0),

                CONSTRAINT fk_materials_unit
                    FOREIGN KEY (unit_id)
                    REFERENCES units_measure (unit_id)
            );
            """;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
