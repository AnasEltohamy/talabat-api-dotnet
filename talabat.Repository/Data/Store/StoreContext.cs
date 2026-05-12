using Microsoft.EntityFrameworkCore;
using System.Reflection;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;

namespace talabat.Repository.Data.Store
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<DeliveryMethod> deliveryMethods { get; set; }





    }
}
