using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IDBUnits;

public interface IUnitsMeasure
{
    public bool Add(UnitsMeasure unit);
    public UnitsMeasure? GetById(int unitId);
    public List<UnitsMeasure> GetAll();
    public bool Update(UnitsMeasure unit);
    public bool Delete(int unitId);

    public UnitsMeasure? GetByName(string name);
}
