using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Repository.Data.Store;

namespace talabat.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable ReposatoryCollection;
        public UnitOfWork(StoreContext Context) 
        {
            _context = Context;
            ReposatoryCollection = new Hashtable();
        }


        public IGenericRepositry<TEntity> CreateRepo<TEntity>()  where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name;
            if (!ReposatoryCollection.ContainsKey(Key))
            {
                var Repo = new GenericRepositores<TEntity>(_context);

                ReposatoryCollection.Add(Key, Repo);
            }
            return ReposatoryCollection[Key] as IGenericRepositry<TEntity> ;
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync();
        }
    }
}
