using System.ComponentModel.DataAnnotations;
using talabat.Core.Entites.Products;

namespace talabat.API.DTOs.BasketAndOrderDtos
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }



    }
}
