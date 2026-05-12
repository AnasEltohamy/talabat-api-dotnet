using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;
using talabat.Core.Specifications.Contract;

namespace talabat.Repository.Data.Store.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T , object >> OrderBy { get; set; }
        public Expression<Func<T , object >> OrderByDec { get; set; }
        public int Skip { get; set; }
        public int Take {  get; set; }
        public bool IsPagination { get; set; }



        public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }

        public void orderBy(Expression<Func<T, object>> expression)
        {
            OrderBy = expression;
        }


        public void orderByDec(Expression<Func<T, object>> expression)
        {
            OrderByDec = expression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }




    }
}
