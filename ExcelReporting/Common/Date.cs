using System.Globalization;

namespace ExcelReporting.Common;

public class Date
{
    private const string DefaultDateFormat = "dd.MM.yyyy";
    
    public int Year { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public Date()
    {
    }
    
    public Date(DateTime dateTime)
    {
        Year = dateTime.Year;
        Month = dateTime.Month;
        Day = dateTime.Day;
    }
    
    public DateTime ToDateTime() => new(Year, Month, Day);

    public static Date ParseFromAnyFormat(string value)
    {
        if (TryParse(value, out var date))
            return date;

        if (TryParseFromOADate(value, out date))
            return date;

        throw new ArgumentException($"Unexpected value: {value}", nameof(value));
    }
    
    public static bool TryParseFromOADate(string value, out Date date)
    {
        date = default!;
        if (long.TryParse(value, out var parsedValue))
        {
            date = new Date(DateTime.FromOADate(parsedValue));
            return true;
        }

        return false;
    }
    
    public override string ToString()
    {
        return $"{Day.ToString().PadLeft(2, '0')}.{Month.ToString().PadLeft(2, '0')}.{Year}";
    }
    
    public static bool TryParse(string value, out Date date)
    {
        if (!DateTime.TryParseExact(value, DefaultDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateTime))
        {
            date = default!;
            return false;
        }

        date = new Date(dateTime);
        return true;
    }
}