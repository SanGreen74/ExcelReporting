using ExcelReporting.Common;
using ExcelReporting.PkoReport;
using Microsoft.Extensions.Logging;

namespace ExcelReporting.Api.Features.Pko.CalculateNext;

public class PkoNextWorksheetCalculator
{
    private readonly ILogger<PkoNextWorksheetCalculator> logger;

    public PkoNextWorksheetCalculator(ILogger<PkoNextWorksheetCalculator> logger)
    {
        this.logger = logger;
    }

    public PkoCalculateNextResponse CalculateNext(PkoCalculateNextRequest request)
    {
        var excelContent = request.ExcelContent;
        var package = ExcelReportPackageProvider.Get(excelContent);
        var excelReport = PkoExcelReportStarter.StartNew(package);

        excelReport
            .UpdateDebit(request.Debit.Roubles, request.Debit.Kopecks)
            .UpdateAcceptedBy(request.AcceptedByPerson)
            .UpdateComplicationDate(request.ComplicationDate)
            .UpdateDocumentNumber(request.DocumentNumber)
            .UpdateZCause(request.ZCauseNumber, request.ComplicationDate);
        excelReport.Save();

        return new PkoCalculateNextResponse
        {
            ExcelContent = excelReport.GetResult()
        };
    }
}