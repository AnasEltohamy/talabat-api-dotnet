namespace talabat.Core.Entites.Products
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId {  get; set; } // forign key => ProductBrand
        public ProductBrand Brand { get; set; } // navigational proberty of one 


        public int CategoryId { get; set; } // forign key => ProductCategory
        public ProductCategory Category { get; set; } // navigational proberty of one 

    }
}
