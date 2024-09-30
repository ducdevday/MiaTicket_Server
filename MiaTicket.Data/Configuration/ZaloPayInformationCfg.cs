using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Configuration
{
    public class ZaloPayInformationCfg : IEntityTypeConfiguration<ZaloPayInformation>
    {
        public void Configure(EntityTypeBuilder<ZaloPayInformation> builder)
        {
            builder.ToTable("ZaloPayInformation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TransactionCode).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PaymentUrl).IsRequired();
            builder.Property(x => x.TotalAmount).IsRequired();
            builder.Property(x => x.PaymentStatus).HasDefaultValue(PaymentStatus.UnPaid);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ExpireAt).IsRequired();
            builder.HasOne(x => x.Order).WithOne(x => x.ZaloPayInformation).HasForeignKey<ZaloPayInformation>(x => x.OrderId);
        }
    }
}