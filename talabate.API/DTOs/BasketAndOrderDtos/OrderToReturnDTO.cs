using talabat.Core.Entites.Order_Aggregate;

namespace talabat.API.DTOs.BasketAndOrderDtos
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; } = new HashSet<OrderItemDTO>();

        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } 

    }
}

 