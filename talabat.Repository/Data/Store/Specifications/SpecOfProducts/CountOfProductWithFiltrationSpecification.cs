using talabat.Core.Entites.Products;

namespace talabat.Repository.Data.Store.Specifications.SpecOfProducts
{
    public class CountOfProductWithFiltrationSpecification : BaseSpecifications<Product>
    {
        public CountOfProductWithFiltrationSpecification(ProductSpecificationParams ProductParams) : base
            (
                p => (!ProductParams.PreandId.HasValue || ProductParams.PreandId == p.BrandId) &&
                   (!ProductParams.CategoryId.HasValue || ProductParams.CategoryId == p.CategoryId) &&
                   (string.IsNullOrEmpty(ProductParams.Search) || p.Name.ToLower().Contains(ProductParams.Search.ToLower()))
            )
        {

        }

    }
}
