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
        public Task<User?> GetAccountByEmail(string email);
        public Task<User?> GetAccountById(Guid uId);
        public Task<bool> ChangePassword(Guid uId, byte[] password);
        public Task<User?> UpdateAccount(Guid uId, string name, string phoneNumber, DateTime birthDate, int gender, string? avatarUrl);
        public Task<User?> ActivateAccount(string email);
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
            var userList = _context.User.Where(x => x.Email == email).ToList();

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
                Role = Role.User,
            });
            return Task.FromResult(addedEntity.Entity);
        }

        public Task<User?> GetAccountByEmail(string email)
        {
            var entity = _context.User.Where(x => x.Email == email).FirstOrDefault();
            return Task.FromResult(entity);
        }

        public Task<User?> GetAccountById(Guid uId)
        {
            return Task.FromResult(_context.User.FirstOrDefault(x => x.Id == uId));
        }

        public Task<bool> ChangePassword(Guid uId, byte[] password)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == uId);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            user.Password = password;
            _context.User.Update(user);
            return Task.FromResult(true);
        }

        public Task<User?> UpdateAccount(Guid uId, string name, string phoneNumber, DateTime birthDate, int gender, string? avatarUrl)
        {

            var user = _context.User.FirstOrDefault(x => x.Id == uId);
            if (user == null)
            {
                return Task.FromResult<User?>(null);
            }
            user.Name = name;
            user.PhoneNumber = phoneNumber;
            user.BirthDate = birthDate;
            user.Gender = (Gender)gender;
            if (avatarUrl != null) user.AvatarUrl = avatarUrl;
            var updatedEntity = _context.User.Update(user);
            return Task.FromResult(updatedEntity.Entity);
        }

        public Task<User?> ActivateAccount(string email) {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            if (user == null) {
                return Task.FromResult<User?>(null);
            }
            user.Status = UserStatus.Active;
            var updatedEntity = _context.User.Update(user);
            return Task.FromResult(updatedEntity.Entity);
        }
    }
}
