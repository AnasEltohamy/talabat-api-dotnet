using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using talabat.Core.Entites.Products;

namespace talabat.Repository.Data.Store.Config
{
    internal class BreandConfigrations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(P=>P.Name).IsRequired();
        }
    }
}
