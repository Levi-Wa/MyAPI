using System.Text.RegularExpressions;

namespace MyAPP.Common.DB.Entity;

public class Units
{
    private string unitName = string.Empty;
    private const string unitNamePattern = "^[А-Яа-я ]{3,50}$"; 
    private string unitShortName = string.Empty;
    private const string unitShortNamePattern = "^[А-Яа-я]{1,4}[.]$"; 
    public int UnitsID { get; set; }
    public string UnitName
    {
        get
        {
            return unitName;
        }
        set
        {
            Regex regex = new Regex(unitNamePattern);
            if (regex.IsMatch(value))
            {
                unitName = value;
            }
            else
            {
                throw new ArgumentException("Not valid unit name!");
            }
        }
    }
    public string UnitShortName
    {
        get
        {
            return unitShortName;
        }
        set
        {
            Regex regex = new Regex(unitShortNamePattern);
            if (regex.IsMatch(value))
            {
                unitShortName = value;
            }
            else
            {
                throw new ArgumentException("Not valid unit short name!");
            }
        }
    }
}