namespace SpendTracerApi.Models.Dtos
{
    public class ResponseDto
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public object? result { get; set; }
    }
}
