using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.SQLite.Tables;

public class DocumentsTable : IDocuments
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS documents (
            document_id INTEGER PRIMARY KEY AUTOINCREMENT,
            document_number TEXT NOT NULL,
            record_date TEXT NOT NULL DEFAULT (datetime('now')),
            counterparty_id INTEGER NOT NULL,
            document_type INTEGER NOT NULL,
            FOREIGN KEY (counterparty_id) REFERENCES counterparties (counterparty_id)
        );
        """;

    private readonly SqliteExecutor<Documents> _executor;

    public DocumentsTable(SqliteExecutor<Documents> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public bool Add(Documents document) => throw new NotImplementedException();
    public Documents? GetById(int documentId) => throw new NotImplementedException();
    public List<Documents> GetAll() => throw new NotImplementedException();
    public bool Update(Documents document) => throw new NotImplementedException();
    public bool Delete(int documentId) => throw new NotImplementedException();
    public List<Documents> GetByDateRange(DateTime from, DateTime to) => throw new NotImplementedException();
    public List<Documents> GetByCounterparty(int counterpartyId) => throw new NotImplementedException();
    public List<Documents> GetByType(int documentType) => throw new NotImplementedException();
}
