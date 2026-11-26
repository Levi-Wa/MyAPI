using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class MaterialsTable : IMaterials
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS materials (
            catalog_id INTEGER PRIMARY KEY AUTOINCREMENT,
            catalog_number TEXT NOT NULL,
            name TEXT NOT NULL,
            grade TEXT NOT NULL,
            profile_size TEXT NOT NULL,
            unit_id INTEGER NOT NULL,
            stock_norm NUMERIC NOT NULL DEFAULT 0,
            FOREIGN KEY (unit_id) REFERENCES units_measure (unit_id)
        );
        """;

    private readonly SqliteExecutor<Materials> _executor;

    public MaterialsTable(SqliteExecutor<Materials> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public bool Add(Materials material) => throw new NotImplementedException();
    public Materials? GetById(int catalogId) => throw new NotImplementedException();
    public List<Materials> GetAll() => throw new NotImplementedException();
    public bool Update(Materials material) => throw new NotImplementedException();
    public bool Delete(int catalogId) => throw new NotImplementedException();
    public List<Materials> GetByName(string name) => throw new NotImplementedException();
    public List<Materials> GetByUnitId(int unitId) => throw new NotImplementedException();
    public decimal GetStockNorm(int catalogId) => throw new NotImplementedException();
}
