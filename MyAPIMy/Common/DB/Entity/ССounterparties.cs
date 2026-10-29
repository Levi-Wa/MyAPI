using System.Text.RegularExpressions;

namespace MyAPP.Common.DB.Entity;

public class Counterparties
{
    public required uint CounterpartyId {get; set;}
    private string name = string.Empty;
    private const string namePattern = @"^[А-Яа-яA-Za-z0-9\s\-\.]{3,100}$";

    private string type = string.Empty;
    private const string typePattern = @"^[А-Яа-яA-Za-z\s]{3,50}$";


    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            Regex regex = new Regex(namePattern);
            if (regex.IsMatch(value))
            {
                name = value;
            }
            else
            {
                throw new ArgumentException("Not valid counterparty name!");
            }
        }
    }

    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            Regex regex = new Regex(typePattern);
            if (regex.IsMatch(value))
            {
                type = value;
            }
            else
            {
                throw new ArgumentException("Not valid counterparty type!");
            }
        }
    }
}