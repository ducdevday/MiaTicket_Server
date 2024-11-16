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
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.VideoUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.Event).WithOne(x => x.Banner).HasForeignKey<Banner>(x => x.EventId);
        }
    }
}
