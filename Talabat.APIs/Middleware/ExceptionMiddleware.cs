using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middleware
{
    public class ExceptionMiddleware
    {
        
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next ,ILogger<ExceptionMiddleware> logger ,IHostEnvironment environment )
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext); 
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, ex.Message);
                // in production =>Log ex in Database
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 500;

                
                if (environment.IsDevelopment())
                {
                    var Response = new ApiExceptionResponse(500 , ex.Message ,ex.StackTrace.ToString());
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var jsonResponse = JsonSerializer.Serialize(Response,options);
                    await httpContext.Response.WriteAsync(jsonResponse);
                }
                else
                {
                    var Response = new ApiExceptionResponse(500);

                }

                





            }
        }






    }
}
