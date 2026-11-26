using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class UnitsMeasureTable : IUnitsMeasure
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS units_measure (
            unit_id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            symbol TEXT NOT NULL
        );
        """;

    private readonly SqliteExecutor<UnitsMeasure> _executor;

    public UnitsMeasureTable(SqliteExecutor<UnitsMeasure> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public bool Add(UnitsMeasure unit) => throw new NotImplementedException();
    public UnitsMeasure? GetById(int unitId) => throw new NotImplementedException();
    public List<UnitsMeasure> GetAll() => throw new NotImplementedException();
    public bool Update(UnitsMeasure unit) => throw new NotImplementedException();
    public bool Delete(int unitId) => throw new NotImplementedException();
    public UnitsMeasure? GetByName(string name) => throw new NotImplementedException();
}
