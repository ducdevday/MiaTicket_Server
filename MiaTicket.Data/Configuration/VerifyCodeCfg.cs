﻿using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Configuration
{
    public class VerifyCodeCfg : IEntityTypeConfiguration<VerifyCode>
    {
        public void Configure(EntityTypeBuilder<VerifyCode> builder)
        {
            builder.ToTable("VerifyCode");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(32);
            builder.Property(x => x.ExpireAt).IsRequired();
            builder.Property(x => x.IsUsed).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.Type).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.VerifyCodes).HasForeignKey(x => x.UserId);
        }
    }
}
