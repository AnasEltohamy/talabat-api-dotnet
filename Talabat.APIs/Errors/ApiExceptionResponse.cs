namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse: ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int Code, string? message = null, string? details = null) : base( Code, message)
        {
            Details = details;



        }
    }
}
