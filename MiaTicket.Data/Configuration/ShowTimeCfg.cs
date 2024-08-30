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
    public class ShowTimeCfg : IEntityTypeConfiguration<ShowTime>
    {
        public void Configure(EntityTypeBuilder<ShowTime> builder)
        {
            builder.ToTable("ShowTime");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ShowStartAt);
            builder.Property(x => x.ShowEndAt);
            builder.Property(x => x.ShowStartAt).IsRequired();
            builder.Property(x => x.ShowEndAt).IsRequired();
            builder.HasOne(x => x.Event).WithMany(x => x.ShowTimes).HasForeignKey(x => x.EventId);
        }
    }
}
