using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites;

namespace Talabat.APIs.DTOs
{
    public class ProductToReturnDTOs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }

    }
}
