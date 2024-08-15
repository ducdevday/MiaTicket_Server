using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiaTicket.Data.Entity;
namespace MiaTicket.DataAccess.Data
{
    public interface IRefreshTokenData
    {
        public Task SaveToken(string token, Guid userId);
        public Task DisableAllTokenByUserId(Guid userId);
        public Task ClearAllTokenByUserId(Guid userId);
        public Task<RefreshToken?> GetLastTokenByUserId(Guid userId);
    }

    public class RefreshTokenData : IRefreshTokenData
    {
        private readonly MiaTicketDBContext _context;

        public RefreshTokenData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task SaveToken(string token, Guid userId)
        {
            _context.RefreshToken.Add(new RefreshToken(
                )
            {
                Value = token,
                UserId = userId,
                IsDisable = false

            });
            return Task.CompletedTask;
        }

        public Task ClearAllTokenByUserId(Guid userId)
        {
            var tokens = _context.RefreshToken.Where(x => x.UserId == userId);
            if (tokens.Any()) {
                _context.RefreshToken.RemoveRange(tokens);
            }
            return Task.CompletedTask;
        }

        public Task<RefreshToken?> GetLastTokenByUserId(Guid userId)
        {
            var refreshToken = _context.RefreshToken.OrderBy(x => x.Id).LastOrDefault(x => x.UserId == userId);
            if (refreshToken != null )
            {
                return Task.FromResult<RefreshToken?>(refreshToken);
            }
            return Task.FromResult<RefreshToken?>(null);
        }

        public Task DisableAllTokenByUserId(Guid userId)
        {
            _context.RefreshToken.Where(x => x.UserId == userId).ToList().ForEach(i => i.IsDisable = true);
            return Task.CompletedTask;
        }
    }
}
