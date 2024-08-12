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
    public class VoucherCfg : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(20);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.TotalLimit);
            builder.Property(x => x.MinQuanityPerOrder);
            builder.Property(x => x.MaxQuanityPerOrder);
            builder.Property(x => x.IsPercentage).IsRequired();
            builder.HasOne(x => x.Event).WithMany(x => x.Vouchers).HasForeignKey(x => x.EventId);
        }
    }
}
