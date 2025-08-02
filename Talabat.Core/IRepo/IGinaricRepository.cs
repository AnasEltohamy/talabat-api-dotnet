using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.IRepo
{
    public interface IGinaricRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> getAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec);

        Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec);
    }
}
