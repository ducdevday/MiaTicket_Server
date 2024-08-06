using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiaTicket.Data.Configuration
{
    public class BannerCfg : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.ToTable("Banner");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.VideoUrl).IsRequired().HasMaxLength(255);
            builder.HasOne(x => x.Event).WithOne(x => x.Banner).HasForeignKey<Banner>(x => x.EventId);
        }
    }
}
