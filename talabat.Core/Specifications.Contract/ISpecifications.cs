using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Products;

namespace talabat.Core.Specifications.Contract
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T,bool>> Criteria { get; set; }

        public List<Expression<Func<T,object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDec { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }

    }
}
