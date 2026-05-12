using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using talabat.Core.Entites.Products;

namespace talabat.Repository.Data.Store.Config
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.Property(P => P.Description).IsRequired();
            builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P=>P.BrandId);
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(P=>P.CategoryId);

        }
    }
}
