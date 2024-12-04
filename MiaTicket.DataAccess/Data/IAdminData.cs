using MiaTicket.Data;
using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IAdminData
    {
        public Task<Admin?> GetAdmin(string account);
        public Task<Admin> UpdateAdmin(Admin admin);
    }

    public class AdminData : IAdminData {
        private readonly MiaTicketDBContext _context;

        public AdminData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Admin?> GetAdmin(string account)
        {
            var admin = _context.Admin.FirstOrDefault(x => x.Account == account);
            return Task.FromResult(admin);
        }

        public Task<Admin> UpdateAdmin(Admin admin)
        {
            var updatedAdmin = _context.Admin.Update(admin);
            return Task.FromResult(updatedAdmin.Entity);
        }
    }
}
