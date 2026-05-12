using AutoMapper;
using AutoMapper.Execution;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;

namespace talabat.API.Helpers
{
    public class OrderPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (source is null)
            {
                return string.Empty;
            }
            else
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";
            }
        }
    }
}
