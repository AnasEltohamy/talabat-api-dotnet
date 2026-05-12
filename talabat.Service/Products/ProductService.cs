using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Core.Services.Contract;
using talabat.Core.Specifications.Contract;
using talabat.Repository.Data.Store.Specifications.SpecOfProducts;

namespace talabat.Service.Products
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        

        public async Task<int> GetCountWithFiltrationAsync(ISpecifications<Product> CountOfSpec)
        {
            return await _unitOfWork.CreateRepo<Product>().GetCountWithFiltrationAsync(CountOfSpec);
        }

        public async Task<Product?> GetProductById(int id)
        {
          return await _unitOfWork.CreateRepo<Product>().GetByIDAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _unitOfWork.CreateRepo<Product>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsWithSpecAsync(ISpecifications<Product> Spec)
        {
            return await _unitOfWork.CreateRepo<Product>().GetAllWithSpecAsync(Spec);
        }

        public async Task<Product> GetProductWithSpecById(ISpecifications<Product> Spec)
        {
            return await _unitOfWork.CreateRepo<Product>().GetByIdWithSpecAsync(Spec);
        }




        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandAsync()
        {
            return await _unitOfWork.CreateRepo<ProductBrand>().GetAllAsync();
        }

        public async Task<IReadOnlyList<ProductCategory>> GetAllCategoryAsync()
        {
           return await _unitOfWork.CreateRepo<ProductCategory>().GetAllAsync();
        }
    }
}
