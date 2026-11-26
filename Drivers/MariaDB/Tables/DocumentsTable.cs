using MyAPP.Common.DB.Entity;
using MyAPP.Common.DB.IDBUnits;

namespace MyAPP.Drivers.MariaDB.Tables;

public class DocumentsTable : IDocuments
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS documents (
            document_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            document_number VARCHAR(50) NOT NULL,
            record_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
            counterparty_id INT UNSIGNED NOT NULL,
            document_type INT NOT NULL,
            FOREIGN KEY (counterparty_id) REFERENCES counterparties (counterparty_id)
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        """;

    private readonly MariaDbExecutor<Documents> _executor;

    public DocumentsTable(MariaDbExecutor<Documents> executor)
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
