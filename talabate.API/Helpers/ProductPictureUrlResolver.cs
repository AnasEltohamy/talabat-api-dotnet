using AutoMapper;
using AutoMapper.Execution;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.Core.Entites.Products;

namespace talabat.API.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (source.PictureUrl is not null )
            {
                    return $"{configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
