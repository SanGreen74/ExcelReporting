// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using ExcelReporting;
using ExcelReporting.Client;
using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var roubles = 17882;
var kopecks = 7;
var kopecksString = kopecks.ToString().PadLeft(2, '0');
var м1 = RusNumber.Str(roubles).Trim();
var м2 = RusNumber.Case(roubles, "рубль", "рубля", "рублей").Trim();
var roublesCyrillic = м1 + " " + м2;
var kopecksCyrillic = RusNumber.Case(kopecks, "копейка", "копейки", "копеек");
var t = $"{roublesCyrillic} {kopecksString} {kopecksCyrillic}";

using (var ep = new ExcelPackage("pko.xlsx"))
{
    var excelReport = PkoExcelReportStarter.StartNew(ep);
    excelReport
        .UpdateComplicationDate(new Date(2022, 9, 9))
        .UpdateDocumentNumber(366)
        .UpdateDebit(17651, 07)
        .UpdateAcceptedBy("Сатин Н.А.")
        .UpdateZCause(356, new Date(2022, 8, 8))
        .Save();
}