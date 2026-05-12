

namespace talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMassage { get; set; }


        public ApiResponse(int statusCode , string? errorMassage = null)
        {
            StatusCode = statusCode;
            ErrorMassage = errorMassage ?? GetDefultMessageForStatusCode(statusCode);
        }

      
        private string? GetDefultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
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
