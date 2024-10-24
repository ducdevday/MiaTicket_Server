using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiaTicket.Data.Configuration
{
    public class OrderCfg : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TotalPrice).HasDefaultValue(0);
            builder.Property(x => x.Discount).HasDefaultValue(0);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.QrCode).HasMaxLength(8).IsRequired();
            builder.Property(x => x.IsUsed).HasDefaultValue(false);
            builder.Property(x => x.OrderStatus).HasDefaultValue(OrderStatus.Pending);
            builder.HasOne(x => x.Event).WithMany(x => x.Orders).HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ShowTime).WithMany(x => x.Orders).HasForeignKey(x => x.ShowTimeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
