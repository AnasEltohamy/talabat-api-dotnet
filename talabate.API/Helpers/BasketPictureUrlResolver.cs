using AutoMapper;
using AutoMapper.Execution;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;

namespace talabat.API.Helpers
{
    public class BasketPictureUrlResolver : IValueResolver<BasketItem, BasketItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public BasketPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(BasketItem source, BasketItemDto destination, string destMember, ResolutionContext context)
        {
            if (source.PictureUrl is not null)
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
