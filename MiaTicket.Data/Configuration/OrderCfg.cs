using MiaTicket.Data.Entity;
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
            builder.Property(x => x.EventName).HasMaxLength(255).IsRequired().IsUnicode();
            builder.Property(x => x.IsOffline).IsRequired();
            builder.Property(x => x.AddressName).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressNo).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressWard).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressDistinct).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressProvince).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.BackgroundUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.LogoUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CategoryName).HasMaxLength(255).IsRequired().IsUnicode();
            builder.Property(x => x.OrganizerName).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.OrganizerInformation).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.OrganizerLogoUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DateStart).IsRequired();
            builder.Property(x => x.DateEnd).IsRequired();
            builder.Property(x => x.Discount);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()") // Set default value to current date
            .ValueGeneratedOnAdd() // automatically set on entity creation
            .HasAnnotation("Timestamp", "CreatedDate");
            builder.Property(x => x.QrCode).IsRequired();
            builder.Property(x => x.QrUrl);
            builder.Property(x => x.PaymentType).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired();
            builder.HasOne(x => x.Event).WithOne(x => x.Order).HasForeignKey<Order>(x => x.EventId);
            builder.HasOne(x => x.User).WithOne(x => x.Order).HasForeignKey<Order>(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
