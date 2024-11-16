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
    public class PaymentCfg : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TransactionCode).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PaymentUrl).IsRequired();
            builder.Property(x => x.TotalAmount).IsRequired();
            builder.Property(x => x.PaymentStatus).HasDefaultValue(PaymentStatus.UnPaid);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ExpireAt).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.Order).WithOne(x => x.Payment).HasForeignKey<Payment>(x => x.OrderId);
        }
    }
}
