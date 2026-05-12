using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites;
using talabat.Core.Entites.Order_Aggregate;

namespace talabat.Repository.Data.Store.Config
{
    internal class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(pi => pi.Product,Product=> Product.WithOwner());

            builder.Property(S => S.Price).HasColumnType("decimal(18,2)");
        }
    }
}
