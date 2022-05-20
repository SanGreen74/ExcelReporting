using ExcelReporting.Common;

namespace ExcelReporting.Api.Features.Pko.CalculateNext
{
    public class PkoCalculateNextRequest
    {
        public byte[] ExcelContent { get; set; } = default!;
        
        public Date ComplicationDate { get; set; }
        
        public int DocumentNumber { get; set; }
        
        public int ZCauseNumber { get; set; }

        public string AcceptedByPerson { get; set; } = default!;

        public Currency Debit { get; set; } = default!;
    }
}