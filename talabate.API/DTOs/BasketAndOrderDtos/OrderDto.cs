using System.ComponentModel.DataAnnotations;

namespace talabat.API.DTOs.BasketAndOrderDtos
{
    public class OrderDto
    {

        //[Required]
        //public string BuyerEmail { get; set; }


        [Required]
        public string BasketId { get; set; }
      
        [Required]
        public AddressDto shipToAddress { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
    
        public string? PaymentIntentId { get; set; }

    }
}
