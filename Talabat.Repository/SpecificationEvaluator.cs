using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity 
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);  
            }


            if (spec.orderBy is not null)
            {
                query = query.OrderBy(spec.orderBy);
            }
            if (spec.orderByDesc is not null)
            {
                query = query.OrderByDescending(spec.orderByDesc);
            }
            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Tack);
            }




            //collection.Aggregate(initialValue, (accumulator, currentItem) => doSomething);
            query = spec.Includes.Aggregate(query, (current, include)=> current.Include(include));


            return query; 
        }
    }
}
