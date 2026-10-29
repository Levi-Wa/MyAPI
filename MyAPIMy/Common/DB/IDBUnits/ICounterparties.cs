using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IDBUnits;

public interface ICounterparties
{
    public bool Add(Counterparties counterparty);
    public Counterparties? GetById(int counterpartyId);
    public List<Counterparties> GetAll();
    public bool Update(Counterparties counterparty);
    public bool Delete(int counterpartyId);
    
    public List<Counterparties> GetByType(string type);
    public List<Counterparties> GetByName(string name);
}
