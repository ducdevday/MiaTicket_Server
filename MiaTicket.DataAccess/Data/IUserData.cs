using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IUserData
    {
        public Task<bool> IsEmailExist(string email);
        public Task<bool> IsGenderValid(int Gender);
        public Task<User> CreateAccount(string name, string email, byte[] password, string phoneNumber, DateTime birthDate, int gender);
        public Task<User?> GetAccount(string email);
    }

    public class UserData : IUserData
    {
        private readonly MiaTicketDBContext _context;

        public UserData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<bool> IsEmailExist(string email)
        {
            bool isEmailExist = _context.User.Any(x => x.Email == email);
            return Task.FromResult(isEmailExist);
        }

        public Task<bool> IsGenderValid(int gender)
        {
            bool isGenderValid = Enum.IsDefined(typeof(Gender), gender);
            return Task.FromResult(isGenderValid);
        }

        public Task<User> CreateAccount(string name, string email, byte[] password, string phoneNumber, DateTime birthDate, int gender)
        {
            var addedEntity =
            _context.User.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = name,
                AvatarUrl = string.Empty,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                BirthDate = birthDate,
                Gender = (Gender)gender,
                Role = UserRole.User
            });
            return Task.FromResult(addedEntity.Entity);
        }

        public Task<User?> GetAccount(string email)
        {
            var entity = _context.User.Where(x => x.Email == email).FirstOrDefault();
            return Task.FromResult(entity);
        }
    }
}
