using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExcelReporting.Client
{
    public class PkoExcelReportParseResponse
    {
        [JsonConverter(typeof(DateConverter))]
        public Date LastComplicationDate { get; set; }
        
        public int LastDocumentNumber { get; set; }
        
        public int LastZCauseNumber { get; set; }
        
        public List<string> AcceptedByPersons { get; set; }
        
        public string LastAcceptedByPerson { get; set; }
    }
}