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
    public class EventCheckInCfg : IEntityTypeConfiguration<EventCheckIn>
    {
        public void Configure(EntityTypeBuilder<EventCheckIn> builder)
        {
            builder.ToTable("EventCheckIn");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IsCheckedIn).HasDefaultValue(false);
            builder.Property(x => x.CheckedInAt);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.Order).WithOne(x => x.EventCheckIn).HasForeignKey<EventCheckIn>(x =>x.OrderId);
            builder.HasOne(x => x.Organizer).WithMany(x => x.EventCheckIns).HasForeignKey(x => x.OrganizerId);
        }
    }
}
