using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product>
    {
        public ProductWithTypeAndBrandSpecification( ProductSpecParams Params) : base
        (
        p => (!Params.BroductId.HasValue || p.ProductBrandId == Params.BroductId) &&
             (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
        )
        {
            Includes.Add(pp => pp.ProductBrand);
            Includes.Add(pt => pt.ProductType);
            if (Params.Sort is not null)
            {
                switch (Params.Sort)
                {
                    case "PriseAsc" :
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriseDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default : 
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination(Params.PageSize * (Params.pageIndex - 1), Params.PageSize);
            

        }

        public ProductWithTypeAndBrandSpecification(int id):base(P=>P.Id == id)
        {
            Includes.Add(pp => pp.ProductBrand);
            Includes.Add(pt => pt.ProductType);
        }









    }
}
