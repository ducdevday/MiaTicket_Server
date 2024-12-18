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
    public class AdminCfg : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Account).HasMaxLength(255).IsRequired();
            builder.HasIndex(x => x.Account).IsUnique();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.Password).HasMaxLength(255).IsRequired();
            builder.Property(x => x.IsPasswordTemporary).HasDefaultValue(true);
        }
    }
}
