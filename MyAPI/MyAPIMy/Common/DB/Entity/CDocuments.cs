
using System.Text.RegularExpressions;

namespace MyAPP.Common.DB.Entity;

public class Documents
{
    public required uint DocumentId {get; set;}

    private string documentNumber = string.Empty;
    private const string document_numberPattern = "^[А-Яа-я]{1,4}[.]$";

    private DateTime recordDate;

    private int counterpartyId;
    private const string counterparty_idPattern = @"^[1-9]\d{0,8}$";


    public string DocumentNumber
    {
        get
        {
            return documentNumber;
        }
        set
        {
            Regex regex = new Regex(document_numberPattern);
            if (regex.IsMatch(value))
            {
                documentNumber = value;
            }
            else
            {
                throw new ArgumentException("Not valid document number!");
            }
        }
    }

    public DateTime RecordDate
    {
        get
        {
            return recordDate;
        }
        set
        {
            recordDate = value;
        }
    }

    public int CounterpartyId
    {
        get
        {
            return counterpartyId;
        }
        set
        {
            Regex regex = new Regex(counterparty_idPattern);
            if (regex.IsMatch(value.ToString()))
            {
                counterpartyId = value;
            }
            else
            {
                throw new ArgumentException("Not valid counterparty id!");
            }
        }
    }

    public int DocumentType { get; set; }
}
