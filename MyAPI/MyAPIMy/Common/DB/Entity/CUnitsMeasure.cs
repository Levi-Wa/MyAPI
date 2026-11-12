using System.Text.RegularExpressions;
namespace MyAPP.Common.DB.Entity;

public class UnitsMeasure
{
    public required uint UnitId {get; set;}

    private string name = string.Empty;
    private const string namePattern = "^[А-Яа-я ]{3,50}$";

    private string symbol = string.Empty;
    private const string symbolPattern = "^[А-Яа-я ]{1,10}$";

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
                throw new ArgumentException("Not valid unit name!");
            }
        }
    }

    public string Symbol
    {
        get
        {
            return symbol;
        }
        set
        {
            Regex regex = new Regex(symbolPattern);
            if (regex.IsMatch(value))
            {
                symbol = value;
            }
            else
            {
                throw new ArgumentException("Not valid unit symbol!");
            }
        }
    }
}
