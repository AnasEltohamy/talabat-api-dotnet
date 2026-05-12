using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Identity;
using talabat.Core.Entites.Products;

namespace talabat.Core.Entites.Order_Aggregate
{
    public class Order: BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public Address ShippingAddress { get; set; }

        //[ForeignKey("DeliveryMethod")]
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }


        public decimal Subtotal { get; set; }

        public string PaymentIntentId { get; set; } 
        
        public decimal GetTotal => Subtotal + DeliveryMethod.Cost ; 
    
    }
}
