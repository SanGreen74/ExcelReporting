using System;
using System.IO;

namespace ExcelReporting.Client
{
    public class PkoExcelReportClient
    {
        private readonly HttpClient httpClient;

        public PkoExcelReportClient(string baseUri)
        {
            httpClient = new HttpClient(baseUri);
        }

        public OperationResult<PkoExcelReportParseResponse> ParseReport(PkoExcelReportParseRequest request)
        {
            return httpClient.Post<PkoExcelReportParseResponse>("pkoExcelReport/parse", request);
        }
    }
}