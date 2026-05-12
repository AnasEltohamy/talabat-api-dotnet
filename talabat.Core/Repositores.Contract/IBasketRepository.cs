using talabat.Core.Entites.Products;

namespace talabat.Core.Repositores.Contract
{
    public interface IBasketRepository
    {

        Task<CustomerBasket?> GetBasketAsync(string Id);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool?> DeleteBasketAsync(string Id);
    }
}
