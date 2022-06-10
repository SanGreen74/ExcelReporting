namespace ExcelReporting.Api.Features.Pko.CalculateNext
{
    public class PkoCalculateNextResponse
    {
        public string ExcelContentBase64 { get; set; } = default!;
        
        public byte[] Content { get; set; }
    }
}