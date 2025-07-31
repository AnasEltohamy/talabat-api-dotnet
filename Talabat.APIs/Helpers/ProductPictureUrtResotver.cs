using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrtResotver : IValueResolver<Product, ProductToReturnDTOs, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrtResotver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTOs destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }      
                return string.Empty;  
        }
    }
}
