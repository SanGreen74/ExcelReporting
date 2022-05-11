using ExcelReporting.Client;
using OfficeOpenXml;

namespace ExcelReporting.PkoReport;

public class PkoExcelReportParser
{
    private readonly ExcelPackage package;
    private ExcelWorksheet ActualWorkSheet => package.Workbook.Worksheets.Last();
    

    public PkoExcelReportParser(ExcelPackage package)
    {
        this.package = package;
    }

    public Date ParseLastComplicationDate()
    {
        var complicationDateCell = ActualWorkSheet.Cells[PkoExcelReportAddresses.ComplicationDate];
        if (complicationDateCell == null)
        {
            throw new ArgumentNullException(nameof(complicationDateCell), "Not found complication date");
        }

        return ParseCellValue(complicationDateCell, Date.Parse);
    }

    public int ParseLastDocumentNumber()
    {
        var documentNumberCell = ActualWorkSheet.Cells[PkoExcelReportAddresses.DocumentNumber];
        if (documentNumberCell == null)
        {
            throw new ArgumentNullException(nameof(documentNumberCell), "Not found last document number");
        }

        var documentNumberString = ParseCellValue(documentNumberCell, x => x);
        var match = PkoExcelReportParserRegex.DocumentNumber.Match(documentNumberString);
        if (!match.Success)
        {
            throw new ArgumentException($"Unexpected line for document number: {documentNumberString}");
        }

        return int.Parse(match.Groups["documentNumber"].Value);
    }

    public int ParseLastZCauseNumber()
    {
        var zCauseNumberCell = ActualWorkSheet.Cells[PkoExcelReportAddresses.ZCauseNumber];
        if (zCauseNumberCell == null)
        {
            throw new ArgumentNullException(nameof(zCauseNumberCell), "Last Z-cause number not found");
        }

        var zCauseNumberString = ParseCellValue(zCauseNumberCell, x => x);
        var match = PkoExcelReportParserRegex.ZCauseNumber.Match(zCauseNumberString);
        if (!match.Success)
        {
            throw new ArgumentException($"Unexpected line for z-cause number: {zCauseNumberString}");
        }

        return int.Parse(match.Groups["zCauseNumber"].Value);
    }

    public string ParseLastAcceptedByPerson()
    {
        return ParseAcceptedBy(ActualWorkSheet);
    }

    private static string ParseAcceptedBy(ExcelWorksheet worksheet)
    {
        var acceptedByPersonCell = worksheet.Cells[PkoExcelReportAddresses.AcceptedBy];
        if (acceptedByPersonCell == null)
        {
            throw new ArgumentNullException(nameof(acceptedByPersonCell), "Last Z-cause number not found");
        }

        var acceptedBy = ParseCellValue(acceptedByPersonCell, x => x);
        return acceptedBy ?? throw new ArgumentNullException(nameof(acceptedBy), "Not found who accept report");
    }

    public List<string> ParseAcceptedByPersons()
    {
        var worksheets = package.Workbook.Worksheets;
        
        return worksheets
            .Select(ParseAcceptedBy)
            .Distinct()
            .ToList();
    }

    private static T ParseCellValue<T>(ExcelRange range, Func<string, T> parse)
    {
        var value = range.GetCellValue<string>(0, 0);
        return parse(value);
    }
}