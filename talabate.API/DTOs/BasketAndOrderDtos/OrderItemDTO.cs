using talabat.Core.Entites.Order_Aggregate;

namespace talabat.API.DTOs.BasketAndOrderDtos
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
