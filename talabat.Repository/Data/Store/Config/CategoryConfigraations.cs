using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using talabat.Core.Entites.Products;


namespace talabat.Repository.Data.Store.Config
{
    internal class CategoryConfigraations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(P=>P.Name).IsRequired();
        }
    }
}
