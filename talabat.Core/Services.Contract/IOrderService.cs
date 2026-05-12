using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;

namespace talabat.Core.Services.Contract
{
    public interface IOrderService 
    {
        public Task<Order> CreateOrderAsync(string buyerEmail,string basketId , int deleveryMethodId , Address address ,string paymentIntentId);

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail);
        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        
 

    }
}
