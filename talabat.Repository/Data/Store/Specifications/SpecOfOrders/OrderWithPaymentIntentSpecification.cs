using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;
using OrderAggregate = talabat.Core.Entites.Order_Aggregate.Order;

namespace talabat.Repository.Data.Store.Specifications.SpecOfOrders
{
    public class OrderWithPaymentIntentSpecification: BaseSpecifications<OrderAggregate>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) :base(O=>O.PaymentIntentId == paymentIntentId)
        {
            
        }

    }
}
