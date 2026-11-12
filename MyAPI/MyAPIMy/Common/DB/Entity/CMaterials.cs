using System.Text.RegularExpressions;

namespace MyAPP.Common.DB.Entity;

public class Materials
{
    public required uint CatalogId {get; set;}
    private string catalogNumber = string.Empty;
    private const string catalog_numberPattern = @"^[А-Яа-яA-Za-z0-9\-]{1,20}$";

    private string name = string.Empty;
    private const string namePattern = @"^[А-Яа-яA-Za-z0-9\s\-]{1,100}$";

    private string grade = string.Empty;
    private const string gradePattern = @"^[А-Яа-яA-Za-z0-9\s\-]{1,50}$";

    private string profileSize = string.Empty;
    private const string profile_sizePattern = @"^[А-Яа-яA-Za-z0-9\s\-\.×x]{1,50}$";

    private int unitId;
    private const string unit_idPattern = @"^[1-9]\d{0,8}$";

    private decimal stockNorm;
    private const string stock_normPattern = @"^\d{1,10}([.,]\d{1,4})?$";

    public string CatalogNumber
    {
        get
        {
            return catalogNumber;
        }
        set
        {
            Regex regex = new Regex(catalog_numberPattern);
            if (regex.IsMatch(value))
            {
                catalogNumber = value;
            }
            else
            {
                throw new ArgumentException("Not valid catalog number!");
            }
        }
    }

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
                throw new ArgumentException("Not valid material name!");
            }
        }
    }

    public string Grade
    {
        get
        {
            return grade;
        }
        set
        {
            Regex regex = new Regex(gradePattern);
            if (regex.IsMatch(value))
            {
                grade = value;
            }
            else
            {
                throw new ArgumentException("Not valid grade!");
            }
        }
    }

    public string ProfileSize
    {
        get
        {
            return profileSize;
        }
        set
        {
            Regex regex = new Regex(profile_sizePattern);
            if (regex.IsMatch(value))
            {
                profileSize = value;
            }
            else
            {
                throw new ArgumentException("Not valid profile size!");
            }
        }
    }

    public int UnitId
    {
        get
        {
            return unitId;
        }
        set
        {
            Regex regex = new Regex(unit_idPattern);
            if (regex.IsMatch(value.ToString()))
            {
                unitId = value;
            }
            else
            {
                throw new ArgumentException("Not valid unit id!");
            }
        }
    }

    public decimal StockNorm
    {
        get
        {
            return stockNorm;
        }
        set
        {
            Regex regex = new Regex(stock_normPattern);
            if (regex.IsMatch(value.ToString()))
            {
                stockNorm = value;
            }
            else
            {
                throw new ArgumentException("Not valid stock norm!");
            }
        }
    }
}
