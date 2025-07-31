using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile 
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTOs>()
                .ForMember(D=>D.ProductBrand, M=>M.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(D=>D.ProductType, M=>M.MapFrom(S=>S.ProductType.Name))
                .ForMember(D=>D.PictureUrl, M=>M.MapFrom<ProductPictureUrtResotver>());
        }
    }
}

