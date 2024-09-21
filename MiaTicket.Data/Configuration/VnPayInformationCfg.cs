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

    public class VnPayInformationCfg : IEntityTypeConfiguration<VnPayInformation>
    {
        public void Configure(EntityTypeBuilder<VnPayInformation> builder)
        {
            builder.ToTable("VnPayInformation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TransactionCode).IsRequired().HasMaxLength(8);
            builder.Property(x => x.PaymentUrl).IsRequired();
            builder.Property(x => x.TotalAmount).IsRequired();
            builder.Property(x => x.PaymentStatus).HasDefaultValue(PaymentStatus.UnPaid);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ExpireAt).IsRequired();
            builder.HasOne(x => x.Order).WithOne(x => x.VnPayInformation).HasForeignKey<VnPayInformation>(x => x.OrderId);
        }
    }
}
