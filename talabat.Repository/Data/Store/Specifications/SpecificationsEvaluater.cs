using Microsoft.EntityFrameworkCore;
using talabat.Core.Entites.Products;
using talabat.Core.Specifications.Contract;

namespace talabat.Repository.Data.Store.Specifications
{
    internal static class SpecificationsEvaluater<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecifications<T> Spec )
        {
            var Query = inputQuery;
            if (Spec.Criteria != null)
            {
                Query = Query.Where( Spec.Criteria );
            }

            if (Spec.OrderBy is not null)
            {
                Query= Query.OrderBy(Spec.OrderBy);
            }

            if (Spec.OrderByDec is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDec);
            }

            if (Spec.IsPagination)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            Query = Spec.Includes.Aggregate(Query, (current, include) => current.Include(include));

            return Query;




        } 
    }
}
