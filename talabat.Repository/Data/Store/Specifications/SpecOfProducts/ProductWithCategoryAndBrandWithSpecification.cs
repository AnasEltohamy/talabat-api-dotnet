using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;





namespace talabat.Repository.Data.Store.Specifications.SpecOfProducts

{
    public class ProductWithCategoryAndBrandWithSpecification : BaseSpecifications<Product>
    {
        public ProductWithCategoryAndBrandWithSpecification(ProductSpecificationParams productSpecificationParams) : base
            (
                p => (!productSpecificationParams.PreandId.HasValue || p.BrandId == productSpecificationParams.PreandId) &&
                     (!productSpecificationParams.CategoryId.HasValue || p.CategoryId == productSpecificationParams.CategoryId) &&
                     (string.IsNullOrEmpty(productSpecificationParams.Search) || p.Name.ToLower().Contains(productSpecificationParams.Search.ToLower()))
            )
        {
            Includes.Add(P => P.Category);
            Includes.Add(P => P.Brand);

            if (productSpecificationParams.Sort is not null)
            {
                switch (productSpecificationParams.Sort)
                {
                    case "PriseAss":
                        orderBy(P => P.Price);
                        break;
                    case "PriseDec":
                        orderByDec(P => P.Price);
                        break;
                    default:
                        orderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                orderBy(P => P.Name);
            }


            ApplyPagination(productSpecificationParams.pageSize * (productSpecificationParams.PageIndex - 1), productSpecificationParams.pageSize);

        }



        public ProductWithCategoryAndBrandWithSpecification(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.Category);
            Includes.Add(P => P.Brand);
        }
    }
}
