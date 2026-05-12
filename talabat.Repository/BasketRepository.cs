using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Repository.Data.Store.Specifications.SpecOfProducts;

namespace talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _connectionMultiplexer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public BasketRepository(IConnectionMultiplexer connectionMultiplexer ,IUnitOfWork unitOfWork,IConfiguration configuration )
        {
            _connectionMultiplexer = connectionMultiplexer.GetDatabase();
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }



        public async Task<bool?> DeleteBasketAsync(string Id)
        {
         
            return await _connectionMultiplexer.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var CustomerBasket =  await _connectionMultiplexer.StringGetAsync(Id);
            return CustomerBasket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(CustomerBasket);



        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var Repo = _unitOfWork.CreateRepo<Product>();

            var updatedItems = new List<BasketItem>();

            if (customerBasket?.Items is null)
            {
                return null;
            }

            foreach (var item in customerBasket.Items)
            {
                var Spec = new ProductWithCategoryAndBrandWithSpecification(item.Id);
                var product = await Repo.GetByIdWithSpecAsync(Spec);
                if (product == null)
                {
                   return null;
                }
                updatedItems.Add(new BasketItem
                {
                    Id = product.Id,
                    productName = product.Name,
                    PictureUrl= product.PictureUrl,
                   // PictureUrl= $"{_configuration["ApiBaseUrl"]}/{product.PictureUrl}",
                    Brand = product.Brand.Name,
                    type = product.Category.Name,
                    Price =product.Price,
                    Quantity = item.Quantity,
                });
                

            }
            customerBasket.Items = updatedItems;

            var SerializeitionOpject = JsonSerializer.Serialize(customerBasket);
            
            var CustomerBasket = await _connectionMultiplexer.StringSetAsync(customerBasket.Id, SerializeitionOpject ,TimeSpan.FromDays(1));
            return CustomerBasket ? await GetBasketAsync(customerBasket.Id) : null;
        }






    }
}
