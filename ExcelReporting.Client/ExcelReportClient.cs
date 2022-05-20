using ExcelReporting.Client.Pko;

namespace ExcelReporting.Client
{
    public class ExcelReportClient
    {
        public IPkoExcelReportClient Pko { get; }

        public ExcelReportClient(string baseUri)
        {
            var httpClient = new HttpClient($"{baseUri}/api/excelReporting");
            Pko = new PkoClient(httpClient);
        }
    }
}