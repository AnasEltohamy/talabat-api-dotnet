using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.NetworkInformation;
using talabat.Core.Entites.Identity;
using talabat.Core.Entites.Order_Aggregate;
namespace talabat.Repository.Data.Store.Config
{
    internal class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(Address=>Address.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(O => O.Status).HasConversion
                (
                    OStatus=> OStatus.ToString(),
                    OStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus) , OStatus)
                );
            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);


            builder.Property(S => S.Subtotal).HasColumnType("decimal(18,2)");


        }
    }

}
