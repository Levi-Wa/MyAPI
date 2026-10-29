using MyAPP.Common;
using MyAPP.Common.DB;

namespace MyAPP.Driver;

public class Database : IDatabase
{
    public IDBUnits DBUnits => throw new NotImplementedException();
}
