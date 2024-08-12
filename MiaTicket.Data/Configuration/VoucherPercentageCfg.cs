using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiaTicket.Data.Configuration
{
    public class VoucherPercentageCfg : IEntityTypeConfiguration<VoucherPercentage>
    {
        public void Configure(EntityTypeBuilder<VoucherPercentage> builder)
        {
            builder.ToTable("VoucherPercentage");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Value).IsRequired();
            builder.HasOne(x => x.Voucher).WithOne(x => x.VoucherPercentage).HasForeignKey<VoucherPercentage>(x => x.VoucherId);
        }
    }
}
