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
    public class UserCfg : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.AvatarUrl).HasMaxLength(255);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.PhoneNumber).IsUnicode();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(UserStatus.UnVerified);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
