
using Microsoft.AspNetCore.Http;

namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode{ get; set; }
        public string ErrorMessage{ get; set; }


        public ApiResponse(int statusCode , string? errorMessage = null ) 
        {
            StatusCode = statusCode;


            ErrorMessage = errorMessage ?? GetDefultMessageForStatusCode(StatusCode);

        }

        private string? GetDefultMessageForStatusCode(int errorCode)
        {
            return errorCode switch 
            {
                400 => "Bad Request",
                401 => "You are not authorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null

            };
        }
    }
}
