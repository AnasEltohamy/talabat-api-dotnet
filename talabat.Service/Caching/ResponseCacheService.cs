using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Services.Contract;

namespace talabat.Service.Caching
{
    public class ResponseCacheService : IResponseCacheService
    {
        public Task CacheResponseAsync(string cachKey, object response, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCachedResponseAsync(string cachKey)
        {
            throw new NotImplementedException();
        }
    }
}
