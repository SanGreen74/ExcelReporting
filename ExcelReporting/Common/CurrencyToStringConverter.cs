using System.Globalization;

namespace ExcelReporting.Common;

public static class CurrencyToStringConverter
{
    public static decimal ParseToDecimal(int roubles, int kopecks) => decimal.Parse($"{roubles}.{kopecks}", CultureInfo.InvariantCulture);

    public static string ConvertToInWords(int roubles, int kopecks)
    {
        var kopecksString = kopecks.ToString().PadLeft(2, '0');
        var roublesValue = RusNumber.Str(roubles).Trim();
        var kopecksValue = RusNumber.Case(roubles, "рубль", "рубля", "рублей").Trim();

        var roublesCyrillic = $"{roublesValue} {kopecksValue}";
        var kopecksCyrillic = RusNumber.Case(kopecks, "копейка", "копейки", "копеек");
        
        return $"{roublesCyrillic} {kopecksString} {kopecksCyrillic}";
    }
}