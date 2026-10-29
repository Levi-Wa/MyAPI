using MyAPP.Common;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers;

public class Database : IDatabase
{
    public ICounterparties Counterparties { get; set; } = null!;
    public IDocuments Documents { get; set; } = null!;
    public IMaterialMovements MaterialMovements { get; set; } = null!;
    public IMaterials Materials { get; set; } = null!;
    public IUnitsMeasure UnitsMeasure { get; set; } = null!;
}
