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
    public class BankAccountCfg : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccount");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.OwnerName).IsRequired().HasMaxLength(100); ;
            builder.Property(x => x.BankNumber).IsRequired().HasMaxLength(50); ;
            builder.Property(x => x.BankName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.BankBranch).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(x => x.Event).WithOne(x => x.BankAccount).HasForeignKey<BankAccount>(x => x.Id);
        }
    }
}
