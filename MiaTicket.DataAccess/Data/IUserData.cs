using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;

namespace MiaTicket.DataAccess.Data
{
    public interface IUserData
    {
        public Task<bool> IsEmailExist(string email);
        public Task<User> CreateAccount(User user);
        public Task<User?> GetAccountByEmail(string email);
        public Task<User?> GetAccountById(Guid uId);
        public Task<User?> UpdateAccount(User user);
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

        public Task<User> CreateAccount(User user)
        {
            var addedEntity = _context.User.Add(user);
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

        public Task<User?> UpdateAccount(User user)
        {
            var updatedEntity = _context.User.Update(user);
            return Task.FromResult(updatedEntity?.Entity);
        }

        public Task<User?> ActivateAccount(string email) {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            if (user == null) {
                return Task.FromResult<User?>(null);
            }
            user.Status = UserStatus.Active;
            var updatedEntity = _context.User.Update(user);
            return Task.FromResult(updatedEntity?.Entity);
        }
    }
}
