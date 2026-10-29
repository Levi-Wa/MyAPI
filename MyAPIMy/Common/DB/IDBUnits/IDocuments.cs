using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB.IDBUnits;

public interface IDocuments
{
    public bool Add(Documents document); // 
    public Documents? GetById(int documentId);
    public List<Documents> GetAll();
    public bool Update(Documents document);
    public bool Delete(int documentId);

    public List<Documents> GetByDateRange(DateTime from, DateTime to); // найти документ по дате 
    public List<Documents> GetByCounterparty(int counterpartyId); // найти контагента
    public List<Documents> GetByType(int documentType); // найти Тип

}
