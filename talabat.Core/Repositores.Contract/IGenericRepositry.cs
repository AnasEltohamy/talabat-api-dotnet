using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;
using talabat.Core.Specifications.Contract;

namespace talabat.Core.Repositores.Contract
{
    public interface IGenericRepositry<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetByIDAsync(int id);
        
        Task <IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec);
        Task<T> GetEntityWithSpecAsync(ISpecifications<T> Spec);

        Task<int> GetCountWithFiltrationAsync(ISpecifications<T> Spec);

        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);

    }
}
