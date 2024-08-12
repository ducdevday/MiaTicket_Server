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
    public class EventCfg : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(255);
            builder.Property(x => x.IsOffline).IsRequired();
            builder.Property(x => x.AddressName).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressNo).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressWard).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AddressDistinct).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.BackgroundUrl).IsRequired().HasMaxLength(255);
            builder.Property(x=> x.LogoUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.OrganizerName).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.OrganizerInformation).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.OrganizerLogoUrl).IsRequired().HasMaxLength(255); ;
            builder.Property(x => x.PaymentAccount).IsRequired().HasMaxLength(100); ;
            builder.Property(x => x.PaymentNumber).IsRequired().HasMaxLength(50); ;
            builder.Property(x => x.PaymentBankName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.PaymentBankBranch).IsRequired().HasMaxLength(50).IsUnicode();
            builder.HasOne(x => x.Category).WithOne(x => x.Event).HasForeignKey<Event>(x => x.CategoryId);
            builder.HasOne(x => x.User).WithMany(x => x.Events).HasForeignKey(x => x.UserId);
        }
    }
}
