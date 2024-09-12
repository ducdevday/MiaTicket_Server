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
            builder.ToTable("Ticket", t =>
            {
                t.HasCheckConstraint("CK_Ticket_Price_MinValue", "Price >= 0");
                t.HasCheckConstraint("CK_Ticket_Quantity_MinValue", "Quantity >= 1");
                t.HasCheckConstraint("CK_Ticket_MinimumPurchase_MinValue", "MinimumPurchase >= 1");
                t.HasCheckConstraint("CK_Ticket_MaximumPurchase_MinValue", "MaximumPurchase >= MinimumPurchase");
                t.HasCheckConstraint("CK_Ticket_MaximumPurchase_MinValue", "MaximumPurchase <= Quantity");
                t.HasCheckConstraint("CK_Ticket_MaximumPurchase_MinValue", "MinimumPurchase <= Quantity");

            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.MinimumPurchase).IsRequired();
            builder.Property(x => x.MaximumPurchase).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(50).IsUnicode();
            builder.HasOne(x => x.ShowTime).WithMany(x => x.Tickets).HasForeignKey(x => x.ShowTimeId);
        }
    }
}
