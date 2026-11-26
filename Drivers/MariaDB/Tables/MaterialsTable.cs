using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class MaterialsTable : IMaterials
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS materials (
            catalog_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            catalog_number VARCHAR(20) NOT NULL,
            name VARCHAR(100) NOT NULL,
            grade VARCHAR(50) NOT NULL,
            profile_size VARCHAR(50) NOT NULL,
            unit_id INT UNSIGNED NOT NULL,
            stock_norm DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
            FOREIGN KEY (unit_id) REFERENCES units_measure (unit_id)
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly MariaDbExecutor<Materials> _executor;

    public MaterialsTable(MariaDbExecutor<Materials> executor)
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
