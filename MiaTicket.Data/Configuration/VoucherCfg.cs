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
            builder.Property(x => x.Code).IsRequired().HasMaxLength(12);
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.InitQuantity);
            builder.Property(x => x.AppliedQuantity);
            builder.Property(x => x.MinQuantityPerOrder);
            builder.Property(x => x.MaxQuantityPerOrder);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.HasOne(x => x.Event).WithMany(x => x.Vouchers).HasForeignKey(x => x.EventId);
        }
    }
}
