using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domine.Entites.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, NP => NP.WithOwner());
            builder.Property(O => O.Status)
                .HasConversion(
                    OS => OS.ToString(),
                    OS => (OrderStatus) Enum.Parse(typeof(OrderStatus), OS)

                );
            builder.HasMany(O => O.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
