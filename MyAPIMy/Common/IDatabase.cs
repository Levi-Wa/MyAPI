using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Common;

public interface IDatabase
{
    public ICounterparties Counterparties {get;}
    public IDocuments Documents {get;}
    public IMaterialMovements MaterialMovements {get;}
    public IMaterials Materials {get;}
    public IUnitsMeasure UnitsMeasure {get;}
}
