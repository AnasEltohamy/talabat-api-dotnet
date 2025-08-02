using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BroductId { get; set; }
        public int? TypeId { get; set; }

        private int pageSize = 5 ;

        public int PageSize
        {
            get { return pageSize ; }
            set { pageSize  = value > 10 ? 10 : value ; }
        }

        public int pageIndex { get; set; } = 1;


    }
}
