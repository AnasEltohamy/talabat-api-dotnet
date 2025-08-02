using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepo;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GinaricRepository<T> : IGinaricRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GinaricRepository(StoreContext context)
        {
            this.context = context;
        }



        public async Task<IReadOnlyList<T>> getAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>)await context.Products.Include(pp => pp.ProductBrand).Include(pt => pt.ProductType).ToListAsync();
            }
            return await context.Set<T>().ToListAsync();
        }       

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }




        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }


        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
           return  SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }

        public async Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecifications(Spec).CountAsync();
        }
    }
}
