using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;
using talabat.Core.Specifications.Contract;

namespace talabat.Core.Services.Contract
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product?> GetProductById(int id);
        Task<IReadOnlyList<Product>> GetProductsWithSpecAsync(ISpecifications<Product> Spec);
        Task<Product> GetProductWithSpecById(ISpecifications<Product> Spec);
        Task<int> GetCountWithFiltrationAsync(ISpecifications<Product> CountOfSpec);
        Task<IReadOnlyList<ProductBrand>> GetAllBrandAsync(); 
        Task <IReadOnlyList<ProductCategory>> GetAllCategoryAsync();
    }
}
