namespace ExcelReporting.Client.Pko
{
    public class PkoCalculateNextRequest
    {
        public byte[] ExcelContent { get; set; }
        
        public Date ComplicationDate { get; set; }
        
        public int DocumentNumber { get; set; }
        
        public int ZCauseNumber { get; set; }
        
        public string AcceptedByPerson { get; set; }

        public Currency Debit { get; set; }
    }
}