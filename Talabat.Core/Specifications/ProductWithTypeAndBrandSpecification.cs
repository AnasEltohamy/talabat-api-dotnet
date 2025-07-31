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
        public ProductWithTypeAndBrandSpecification(string sort , int? prandId , int? typeId ) : base
        (
        p => (!prandId.HasValue || p.ProductBrandId == prandId) &&
             (!typeId.HasValue || p.ProductTypeId == typeId)
        )
        {
            Includes.Add(pp => pp.ProductBrand);
            Includes.Add(pt => pt.ProductType);
            if (sort is not null)
            {
                switch (sort)
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
        }

        public ProductWithTypeAndBrandSpecification(int id):base(P=>P.Id == id)
        {
            Includes.Add(pp => pp.ProductBrand);
            Includes.Add(pt => pt.ProductType);
        }









    }
}
