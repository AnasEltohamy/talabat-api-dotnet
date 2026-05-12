using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using talabat.Core.Services.Contract;

namespace talabat.API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        



        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Inject ResponseCacheService
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            // Generate Cache Key
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            // Get Cached Response If it Exist
            var cachedResponse = await cachedService.GetCachedResponseAsync(cacheKey);

            // response is not null we will return it to user
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            // Execute next action or the action itself
            var executedEndpointContext = await next();
            if (executedEndpointContext.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }


        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            //{{BaseUrl}}/api/Product?pageSize=5&pageIndex=1&sort=name
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
