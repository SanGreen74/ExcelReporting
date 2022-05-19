using System.Collections.Generic;

namespace ExcelReporting.Client.Pko
{
    public class PkoExcelReportParseResponse
    {
        /// <summary>
        /// Дата составления
        /// </summary>
        public Date LastComplicationDate { get; set; }
        
        /// <summary>
        /// Номер документа в формате КП-00001
        /// </summary>
        public int LastDocumentNumber { get; set; }
        
        /// <summary>
        /// Номер Z отчета
        /// </summary>
        public int LastZCauseNumber { get; set; }
        
        /// <summary>
        /// Список всех людей, когда либо принимавших отчет
        /// </summary>
        public List<string> AcceptedByPersons { get; set; }
        
        /// <summary>
        /// Человек, принявший отчет
        /// </summary>
        public string LastAcceptedByPerson { get; set; }
        
        /// <summary>
        /// Адрес магазина
        /// </summary>
        public string ShopAddress { get; set; }
    }
}