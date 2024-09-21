using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiaTicket.Data.Configuration
{
    public class OrderTicketCfg : IEntityTypeConfiguration<OrderTicket>
    {
        public void Configure(EntityTypeBuilder<OrderTicket> builder)
        {
            builder.ToTable("OrderTicket");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Price).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasOne(x => x.Order).WithMany(x => x.OrderTickets).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Ticket).WithMany(x => x.OrderTickets).HasForeignKey(x => x.TicketId);
        }
    }
}
