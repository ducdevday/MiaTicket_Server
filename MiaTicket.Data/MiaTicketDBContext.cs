using MiaTicket.Data.Configuration;
using MiaTicket.Data.Entity;
using MiaTicket.Setting;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.Data
{
    public class MiaTicketDBContext : DbContext
    {
        private EnviromentSetting _setting = EnviromentSetting.GetInstance();
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
            modelBuilder.ApplyConfiguration(new VerifyCodeCfg());
            modelBuilder.ApplyConfiguration(new VnPayInformationCfg());
            modelBuilder.ApplyConfiguration(new ZaloPayInformationCfg());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_setting.GetConnectionString());
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
        public DbSet<VerifyCode> VerifyCode { get; set; }
        public DbSet<VnPayInformation> VnPayInformation { get; set; }
        public DbSet<ZaloPayInformation> ZaloPayInformation { get; set; }
    }
}
