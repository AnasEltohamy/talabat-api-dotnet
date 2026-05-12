using System.ComponentModel.DataAnnotations;

namespace talabat.API.DTOs.BasketAndOrderDtos
{
    public class BasketItemDto
    {

        [Required] public int Id { set; get; }


        public string? productName { set; get; }

        public string? PictureUrl { set; get; }

        public string? Brand { set; get; }

        public string? type { set; get; }

        public decimal?   Price { set; get; }


        [Required] public int Quantity { set; get; }
    }
}
