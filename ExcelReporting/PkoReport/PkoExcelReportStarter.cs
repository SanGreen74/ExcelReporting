using OfficeOpenXml;

namespace ExcelReporting.PkoReport;

public static class PkoExcelReportStarter
{
    public static PkoExcelReport StartNew(ExcelPackage package)
    {
        var worksheets = package.Workbook.Worksheets;
        var worksheet = worksheets.Last();

        if (!int.TryParse(worksheet.Name, out var worksheetNumber))
        {
            throw new Exception($"Unexpected name: {worksheet.Name}. Can't parse to number");
        }

        var newWorkSheetName = worksheetNumber + 1;
        worksheets.Add(newWorkSheetName.ToString(), worksheets.Last());
        return new PkoExcelReport(package);
    }
}