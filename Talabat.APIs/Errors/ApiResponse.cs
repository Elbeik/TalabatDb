using System;

namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessage(statusCode);
        }

        private string GetDefultMessage(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "bad request",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors ",
                _ => null
            };
        }

        
    }
}
