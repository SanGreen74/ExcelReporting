using ExcelReporting.Common;

namespace ExcelReporting.Api.Features.Pko.CalculateNext
{
    public class PkoCalculateNextResponse
    {
        public Date Date { get; set; }
        
        public byte[] Content { get; set; }
    }
}