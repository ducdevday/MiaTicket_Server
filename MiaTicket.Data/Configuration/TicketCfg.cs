using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Configuration
{
    public class TicketCfg : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.MininumPurchase).IsRequired();
            builder.Property(x => x.MaximumPurchase).IsRequired();
            builder.Property(x => x.SaleStart).IsRequired();
            builder.Property(x => x.SaleEnd).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.ImageUrl).HasMaxLength(255);
            builder.HasOne(x => x.ShowTime).WithMany(x => x.Tickets).HasForeignKey(x => x.ShowTimeId);
        }
    }
}
