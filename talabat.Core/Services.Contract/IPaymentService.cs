using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;

namespace talabat.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<Entites.Order_Aggregate.Order> UpdatePaymentIntentSucceededOrFailed(string paymentIntentId, bool IsSuccess);
        public Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}
