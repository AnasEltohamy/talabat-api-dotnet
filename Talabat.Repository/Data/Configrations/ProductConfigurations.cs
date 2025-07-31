using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data.Configrations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(P=>P.ProductBrandId);
            builder.HasOne( P=>P.ProductType )
                    .WithMany()
                    .HasForeignKey(P=>P.ProductTypeId);

            builder.Property(P=>P.Price).HasColumnType("decimal(18,2)");
            
        }
    }
}
