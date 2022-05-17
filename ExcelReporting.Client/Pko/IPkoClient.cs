namespace ExcelReporting.Client.Pko
{
    public interface IPkoExcelReportClient
    {
        OperationResult<PkoExcelReportParseResponse> ParseReport(PkoExcelReportParseRequest request);

        OperationResult<PkoCalculateNextResponse> CalculateNext(PkoCalculateNextRequest request);
    }
}