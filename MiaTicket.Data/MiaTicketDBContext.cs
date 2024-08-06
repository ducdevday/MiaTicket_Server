using MiaTicket.Data.Configuration;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.Data
{
    public class MiaTicketDBContext : DbContext
    {
        public MiaTicketDBContext() : base()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BannerCfg());
            modelBuilder.ApplyConfiguration(new CategoryCfg());
            modelBuilder.ApplyConfiguration(new EventCfg());
            modelBuilder.ApplyConfiguration(new OrderCfg());
            modelBuilder.ApplyConfiguration(new OrderTicketCfg());
            modelBuilder.ApplyConfiguration(new ShowTimeCfg());
            modelBuilder.ApplyConfiguration(new TicketCfg());
            modelBuilder.ApplyConfiguration(new UserCfg());
            modelBuilder.ApplyConfiguration(new VoucherCfg());
            modelBuilder.ApplyConfiguration(new VoucherFixedAmountCfg());
            modelBuilder.ApplyConfiguration(new VoucherPercentageCfg());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-VEKQUME7\\SQLEXPRESS;Initial Catalog=MiaTicketDB;Integrated Security=True;Trust Server Certificate=True");
        }

        public DbSet<Banner> Banner { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderTicket> OrderTicket {  get; set; }
        public DbSet<ShowTime> ShowTime { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<VoucherFixedAmount> VoucherFixedAmount {  get; set; }
        public DbSet<VoucherPercentage> VoucherPercentage { get; set; }

    }
}
