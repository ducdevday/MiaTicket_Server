using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Configuration
{
    public class EventCfg : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(255);
            builder.Property(x => x.Slug).HasComputedColumnSql("LOWER(CONCAT(REPLACE(Name, ' ', '-'), '-', Id))");
            builder.Property(x => x.Description).IsRequired().IsUnicode();
            builder.Property(x => x.AddressName).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressNo).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressWard).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressDistrict).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.BackgroundUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.LogoUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.OrganizerName).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.OrganizerInformation).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.OrganizerLogoUrl).IsRequired().HasMaxLength(255); ;
            builder.Property(x => x.PaymentAccount).IsRequired().HasMaxLength(100); ;
            builder.Property(x => x.PaymentNumber).IsRequired().HasMaxLength(50); ;
            builder.Property(x => x.PaymentBankName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.PaymentBankBranch).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.Status).HasDefaultValue(EventStatus.Accepted);
            builder.HasOne(x => x.Category).WithMany(x => x.Events).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User).WithMany(x => x.Events).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
