namespace ExcelReporting.Client.Pko
{
    internal class PkoClient : IPkoExcelReportClient
    {
        private readonly HttpClient httpClient;

        public PkoClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public OperationResult<PkoExcelReportParseResponse> ParseReport(PkoExcelReportParseRequest request)
        {
            return httpClient.Post<PkoExcelReportParseResponse>("pko/parse", request);
        }
        
        public OperationResult<PkoCalculateNextResponse> CalculateNext(PkoCalculateNextRequest request)
        {
            return httpClient.Post<PkoCalculateNextResponse>("pko/calculateNext", request);
        }
    }
}