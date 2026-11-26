using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class CounterpartiesTable : ICounterparties
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS counterparties (
            counterparty_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            name VARCHAR(100) NOT NULL,
            type VARCHAR(50) NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly MariaDbExecutor<Counterparties> _executor;

    public CounterpartiesTable(MariaDbExecutor<Counterparties> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public bool Add(Counterparties counterparty) => throw new NotImplementedException();
    public Counterparties? GetById(int counterpartyId) => throw new NotImplementedException();
    public List<Counterparties> GetAll() => throw new NotImplementedException();
    public bool Update(Counterparties counterparty) => throw new NotImplementedException();
    public bool Delete(int counterpartyId) => throw new NotImplementedException();
    public List<Counterparties> GetByType(string type) => throw new NotImplementedException();
    public List<Counterparties> GetByName(string name) => throw new NotImplementedException();
}
