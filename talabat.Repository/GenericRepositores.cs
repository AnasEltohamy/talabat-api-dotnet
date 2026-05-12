using Microsoft.EntityFrameworkCore;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Core.Specifications.Contract;
using talabat.Repository.Data.Store;
using talabat.Repository.Data.Store.Specifications;

namespace talabat.Repository
{
    public class GenericRepositores<T> : IGenericRepositry<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepositores(StoreContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T item)
        {
             await _context.Set<T>().AddAsync(item);
        }
        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }
        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
           return  await SpecificationsEvaluater<T>.GetQuery(_context.Set<T>() ,Spec).ToListAsync();
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec)
        {
            return await SpecificationsEvaluater<T>.GetQuery(_context.Set<T>(), Spec).FirstOrDefaultAsync();
        }

        public async Task<T> GetEntityWithSpecAsync(ISpecifications<T> Spec)
        {
            return await SpecificationsEvaluater<T>.GetQuery(_context.Set<T>(), Spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithFiltrationAsync(ISpecifications<T> Spec)
        {
            return await SpecificationsEvaluater<T>.GetQuery(_context.Set<T>(),Spec).CountAsync();
        }

      
    }
}
