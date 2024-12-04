using MiaTicket.Data.Entity;
using MiaTicket.Setting;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace MiaTicket.Data.Extentions
{
    public static class ModelBuilderExtension
    {
        private static EnviromentSetting _setting = EnviromentSetting.GetInstance();
        public static void Seed(this ModelBuilder modelBuilder) {
            var adminId = Guid.NewGuid();
            var adminPassword = HashPassword(_setting.GetAdminDefaultPassword()); // Hash mật khẩu

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = adminId,
                Account = "admin",
                Password = adminPassword,
            });
        }

        public static byte[] HashPassword(string value)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(value);
            byte[] hashedBytes = Encoding.UTF8.GetBytes(hashedPassword);
            return hashedBytes;
        }
    }
}
