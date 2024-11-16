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
    public class EventOrganizerCfg : IEntityTypeConfiguration<EventOrganizer>
    {
        public void Configure(EntityTypeBuilder<EventOrganizer> builder)
        {
            builder.ToTable("EventOrganizer");
            builder.HasKey(x => new {x.EventId,x.OrganizerId});
            builder.Property(x => x.Position).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne<Event>(x => x.Event).WithMany(x => x.EventOrganizers).HasForeignKey(x => x.EventId);
            builder.HasOne<User>(x => x.Organizer).WithMany(x => x.EventOrganizers).HasForeignKey(x => x.OrganizerId);
        }
    }
}
