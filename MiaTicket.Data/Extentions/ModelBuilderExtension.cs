using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.Data.Extentions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder) {
            //modelBuilder.Entity<User>().HasData(new User());
        }
    }
}
