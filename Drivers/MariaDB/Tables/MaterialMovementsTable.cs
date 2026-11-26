using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class MaterialMovementsTable : IMaterialMovements
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS material_movements (
            record_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            document_id INT UNSIGNED NOT NULL,
            catalog_id INT UNSIGNED NOT NULL,
            document_price DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
            income DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
            expense DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
            FOREIGN KEY (document_id) REFERENCES documents (document_id) ON DELETE CASCADE,
            FOREIGN KEY (catalog_id) REFERENCES materials (catalog_id) ON DELETE RESTRICT
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly MariaDbExecutor<MaterialMovements> _executor;

    public MaterialMovementsTable(MariaDbExecutor<MaterialMovements> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public bool Add(MaterialMovements movement) => throw new NotImplementedException();
    public MaterialMovements? GetById(int recordId) => throw new NotImplementedException();
    public List<MaterialMovements> GetAll() => throw new NotImplementedException();
    public bool Update(MaterialMovements movement) => throw new NotImplementedException();
    public bool Delete(int recordId) => throw new NotImplementedException();
    public List<MaterialMovements> GetByDocument(int documentId) => throw new NotImplementedException();
    public List<MaterialMovements> GetByCatalog(string catalogId) => throw new NotImplementedException();
    public decimal GetBalance(string catalogId) => throw new NotImplementedException();
    public decimal GetTotalIncome(string catalogId, DateTime from, DateTime to) => throw new NotImplementedException();
    public decimal GetTotalExpense(string catalogId, DateTime from, DateTime to) => throw new NotImplementedException();
}
