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
    internal class DelivreyMethodConfiguration : IEntityTypeConfiguration<DelivreyMethod>
    {
        public void Configure(EntityTypeBuilder<DelivreyMethod> builder)
        {
            builder.Property(DM => DM.Cost)
                 .HasColumnType("decimal(18, 2)");
        }
    }
}
