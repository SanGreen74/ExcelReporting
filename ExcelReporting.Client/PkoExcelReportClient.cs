using System.IO;
using Newtonsoft.Json;

namespace ExcelReporting.Client
{
    public class PkoExcelReportClient
    {
        private readonly string baseUri;
        
        public PkoExcelReportClient(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public PkoExcelReportParseResponse ParseReport(PkoExcelReportParseRequest request)
        {
            var httpClient = new HttpClient(baseUri);
            var webResponse = httpClient.Post("pko/parse", request);
            var responseStream = webResponse.GetResponseStream();
            using (var sr = new StreamReader(responseStream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var readToEnd = sr.ReadToEnd();
                var readAsString = jsonTextReader.ReadAsString();
                return JsonConvert.DeserializeObject<PkoExcelReportParseResponse>(readAsString);
            }
        }
    }
}