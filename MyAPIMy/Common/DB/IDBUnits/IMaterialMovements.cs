using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IDBUnits;

public interface IMaterialMovements
{
    public bool Add(MaterialMovements movement);
    public MaterialMovements? GetById(int recordId);
    public List<MaterialMovements> GetAll();
    public bool Update(MaterialMovements movement);
    public bool Delete(int recordId);

    public List<MaterialMovements> GetByDocument(int documentId);
    public List<MaterialMovements> GetByCatalog(string catalogId);

    public decimal GetBalance(string catalogId);
    public decimal GetTotalIncome(string catalogId, DateTime from, DateTime to);
    public decimal GetTotalExpense(string catalogId, DateTime from, DateTime to);
}
