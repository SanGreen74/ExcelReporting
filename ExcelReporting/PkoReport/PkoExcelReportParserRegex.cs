using System.Text.RegularExpressions;

namespace ExcelReporting.PkoReport;

public class PkoExcelReportParserRegex
{
    public static readonly Regex DocumentNumber = new(@"^КП-(?<documentNumber>\d+)$", RegexOptions.Compiled);

    public static readonly Regex ZCauseNumber =
        new(@"^Z-отчет\s№\s(?<zCauseNumber>\d+)\sот\s\d+.\d+.\d+", RegexOptions.Compiled);
}