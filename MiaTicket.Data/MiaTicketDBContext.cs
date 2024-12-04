using MiaTicket.Data.Configuration;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Extentions;
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
            modelBuilder.ApplyConfiguration(new AdminCfg());
            modelBuilder.ApplyConfiguration(new BankAccountCfg());
            modelBuilder.ApplyConfiguration(new BannerCfg());
            modelBuilder.ApplyConfiguration(new CategoryCfg());
            modelBuilder.ApplyConfiguration(new EventCfg());
            modelBuilder.ApplyConfiguration(new EventOrganizerCfg());
            modelBuilder.ApplyConfiguration(new OrderCfg());
            modelBuilder.ApplyConfiguration(new OrderTicketCfg());
            modelBuilder.ApplyConfiguration(new PaymentCfg());
            modelBuilder.ApplyConfiguration(new ShowTimeCfg());
            modelBuilder.ApplyConfiguration(new TicketCfg());
            modelBuilder.ApplyConfiguration(new UserCfg());
            modelBuilder.ApplyConfiguration(new VerificationCodeCfg());
            modelBuilder.ApplyConfiguration(new VoucherCfg());
            modelBuilder.ApplyConfiguration(new EventCfg());

            // Seeding Data
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_setting.GetConnectionString());

        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

            foreach (var entity in modifiedEntities)
            {
                if (entity.Entity.GetType().GetProperty("UpdatedAt") != null)
                {
                    entity.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }



        public DbSet<Admin> Admin { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventOrganizer> EventOrganizer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderTicket> OrderTicket {  get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<ShowTime> ShowTime { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<VerificationCode> VerificationCode { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<EventCheckIn> EventCheckIn { get; set; }
    }
}
