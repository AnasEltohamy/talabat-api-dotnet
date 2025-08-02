using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync: BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParams Params) :base
            (
             p => (!Params.BroductId.HasValue || p.ProductBrandId == Params.BroductId) &&
                  (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
            )
        {
            
        }
    }
}
