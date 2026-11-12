using MyAPP.Common.DB.Entity;

namespace MyAPP.Common.DB;

public interface IDBUnits
{
    public List<Units> Get();
    public Units? Get(int id);
    public void Post (Units item);
    public void Put(int id, Units item);
    public void Delete(int id);
}