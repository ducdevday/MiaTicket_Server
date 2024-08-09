using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IUserData
    {
        public Task<bool> IsAccountExist(string email);
        public Task<bool> CreateAccount(string name, string email, string password, string phoneNumber, DateTime birthDate, int gender);
        public Task<bool> LoginAccount(string email, string password);
    }

    public class UserData : IUserData
    {
        private readonly MiaTicketDBContext _context;

        public UserData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAccountExist(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAccount(String name, string email, string password, string phoneNumber, DateTime birthDate, int gender)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoginAccount(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
