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
    public class VoucherFixedAmountCfg : IEntityTypeConfiguration<VoucherFixedAmount>
    {
        public void Configure(EntityTypeBuilder<VoucherFixedAmount> builder)
        {
            builder.ToTable("VoucherFixedAmount");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Value).IsRequired();
            builder.HasOne(x => x.Voucher).WithOne(x => x.VoucherFixedAmount).HasForeignKey<VoucherFixedAmount>(x => x.VoucherId);
        }
    }
}
