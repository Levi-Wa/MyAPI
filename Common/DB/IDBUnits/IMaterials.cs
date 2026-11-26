using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IDBUnits;

public interface IMaterials
{
    public bool Add(Materials material);
    public Materials? GetById(int catalogId);
    public List<Materials> GetAll();
    public bool Update(Materials material);
    public bool Delete(int catalogId);

    List<Materials> GetByName(string name);
    List<Materials> GetByUnitId(int unitId);
    decimal GetStockNorm(int catalogId);
}
