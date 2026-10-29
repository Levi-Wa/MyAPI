using System.Text.RegularExpressions;

namespace MyAPP.Common.DB.Entity;

public class MaterialMovements
{
    public required uint RecordId {get; set;}

    public required uint DocumentId {get; set;}

    public required uint CatalogId {get; set;}

    private decimal documentPrice;
    private const string document_pricePattern = @"^\d{1,10}([.,]\d{1,4})?$";

    private decimal income;
    private const string incomePattern = @"^\d{1,10}([.,]\d{1,4})?$";

    private decimal expense;
    private const string expensePattern = @"^\d{1,10}([.,]\d{1,4})?$";

    public decimal DocumentPrice
    {
        get
        {
            return documentPrice;
        }
        set
        {
            Regex regex = new Regex(document_pricePattern);
            if (regex.IsMatch(value.ToString()))
            {
                documentPrice = value;
            }
            else
            {
                throw new ArgumentException("Not valid document price!");
            }
        }
    }

    public decimal Income
    {
        get
        {
            return income;
        }
        set
        {
            Regex regex = new Regex(incomePattern);
            if (regex.IsMatch(value.ToString()))
            {
                income = value;
            }
            else
            {
                throw new ArgumentException("Not valid income value!");
            }
        }
    }

    public decimal Expense
    {
        get
        {
            return expense;
        }
        set
        {
            Regex regex = new Regex(expensePattern);
            if (regex.IsMatch(value.ToString()))
            {
                expense = value;
            }
            else
            {
                throw new ArgumentException("Not valid expense value!");
            }
        }
    }
}
