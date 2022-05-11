using System.Collections.Generic;

namespace ExcelReporting.Client
{
    public class PkoExcelReportParseResponse
    {
        public Date LastComplicationDate { get; set; }
        
        public int LastDocumentNumber { get; set; }
        
        public int LastZCauseNumber { get; set; }
        
        public List<string> AcceptedByPersons { get; set; }
        
        public string LastAcceptedByPerson { get; set; }
    }
}