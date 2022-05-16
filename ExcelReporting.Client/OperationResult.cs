namespace ExcelReporting.Client
{
    public class OperationResult<T>
    {
        public T Result { get; set; }
        
        public string ErrorDescription { get; set; }
        
        public int StatusCode { get; set; }
    }
}