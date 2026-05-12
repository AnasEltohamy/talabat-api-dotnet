using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Entites.Products
{
    public class BasketItem
    {
        public int Id { set; get; }
        public string productName { set; get; }

        public string PictureUrl { set; get; }
        public string Brand { set; get; }
        public string type { set; get; }

        public decimal Price { set; get; }

        public int Quantity { set; get; }

    }
}
