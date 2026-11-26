using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class UnitsMeasureTable : IUnitsMeasure
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS units_measure (
            unit_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            name VARCHAR(50) NOT NULL,
            symbol VARCHAR(10) NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly MariaDbExecutor<UnitsMeasure> _executor;

    public UnitsMeasureTable(MariaDbExecutor<UnitsMeasure> executor)
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
