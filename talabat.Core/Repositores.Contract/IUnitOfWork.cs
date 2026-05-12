using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;

namespace talabat.Core.Repositores.Contract
{
    public interface IUnitOfWork:IAsyncDisposable
    {

        IGenericRepositry<TEntity> CreateRepo<TEntity> () where TEntity: BaseEntity;
        Task<int> CompleteAsync(); 

    }
}
