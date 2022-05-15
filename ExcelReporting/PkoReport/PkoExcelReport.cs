using ExcelReporting.Client;
using ExcelReporting.Common;
using OfficeOpenXml;

namespace ExcelReporting.PkoReport;

public class PkoExcelReport : IPkoExcelReport
{
    private readonly ExcelPackage package;
    private ExcelWorksheet ActualWorkSheet => package.Workbook.Worksheets.Last();

    public PkoExcelReport(ExcelPackage package)
    {
        this.package = package;
    }

    public IPkoExcelReport UpdateComplicationDate(Date date)
    {
        var oaDate = date.ToDateTime().ToOADate();
        UpdateCellValue(PkoExcelReportAddresses.ComplicationDate, oaDate);
        return this;
    }

    public IPkoExcelReport UpdateDocumentNumber(int documentNumber)
    {
        //КП-00366
        var documentNumberString = $"КП-{documentNumber.ToString().PadLeft(5, '0')}";
        UpdateCellValue(PkoExcelReportAddresses.DocumentNumber, documentNumberString);
        return this;
    }

    public IPkoExcelReport UpdateDebit(int roubles, int kopecks)
    {
        //Сумма, руб.коп.
        var debit = CurrencyToStringConverter.ParseToDouble(roubles, kopecks);
        UpdateCellValue(PkoExcelReportAddresses.DebitAmount, debit);
        
        var currencyCyrillic = CurrencyToStringConverter.ConvertToInWords(roubles, kopecks);
        UpdateCellValue(PkoExcelReportAddresses.DebitAmountString, currencyCyrillic);
        return this;
    }

    public IPkoExcelReport UpdateZCause(int number, Date date)
    {
        //Z-отчет № 326 от 10.04.2022г.
        var zCauseString = $"Z-отчет № {number} от {date}г.";
        UpdateCellValue(PkoExcelReportAddresses.ZCauseNumber, zCauseString);
        return this;
    }

    public IPkoExcelReport UpdateAcceptedBy(string name)
    {
        UpdateCellValue(PkoExcelReportAddresses.AcceptedBy, name);
        return this;
    }

    public void Save()
    {
        package.Save();
    }

    private void UpdateCellValue<T>(string cellAddress, T newValue)
    {
        var cell = ActualWorkSheet.Cells[cellAddress];
        cell.SetCellValue(0, 0, newValue);
    }
}