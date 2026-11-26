using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class MaterialMovementsTable : IMaterialMovements
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS material_movements (
            record_id INTEGER PRIMARY KEY AUTOINCREMENT,
            document_id INTEGER NOT NULL,
            catalog_id INTEGER NOT NULL,
            document_price NUMERIC NOT NULL DEFAULT 0,
            income NUMERIC NOT NULL DEFAULT 0,
            expense NUMERIC NOT NULL DEFAULT 0,
            FOREIGN KEY (document_id) REFERENCES documents (document_id) ON DELETE CASCADE,
            FOREIGN KEY (catalog_id) REFERENCES materials (catalog_id) ON DELETE RESTRICT
        );
        """;

    private readonly SqliteExecutor<MaterialMovements> _executor;

    public MaterialMovementsTable(SqliteExecutor<MaterialMovements> executor)
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
