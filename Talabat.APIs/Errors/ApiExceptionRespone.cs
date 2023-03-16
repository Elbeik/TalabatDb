namespace Talabat.APIs.Errors
{
    public class ApiExceptionRespone : ApiResponse
    {
        public string Details { get; set; }
        public ApiExceptionRespone(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
