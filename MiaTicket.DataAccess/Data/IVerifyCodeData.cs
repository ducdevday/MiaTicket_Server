using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IVerifyCodeData
    {
        public Task<string> CreateVerifyCode(Guid uid, string code, DateTime expireAt, VerifyType type);
        public Task<bool> IsValidCode(Guid uid,string code, VerifyType type);
        public Task<bool> IsUsedCode(string code);
        public Task UpdateUseOfCode(string code);
    }

    public class VerifyCodeData : IVerifyCodeData
    {
        private readonly MiaTicketDBContext _context;

        public VerifyCodeData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<string> CreateVerifyCode(Guid uid, string code, DateTime expireAt, VerifyType type)
        {
            _context.VerifyCode.Add(new VerifyCode()
            {
                UserId = uid,
                Code = code,
                ExpireAt = expireAt,
                Type = type,
                IsUsed = false,
            });
            return Task.FromResult(code);
        }

        public Task<bool> IsUsedCode(string code)
        {
            var verifyCode = _context.VerifyCode.FirstOrDefault((x) => x.Code == code);
            if (verifyCode == null || verifyCode.IsUsed == false) {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> IsValidCode(Guid uid, string code, VerifyType type)
        {
            var validCode = _context.VerifyCode.OrderBy(x => x.Id).LastOrDefault(x => x.UserId == uid && x.Type == type);
            if (validCode != null && validCode.Code == code && validCode.ExpireAt > DateTime.Now) {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task UpdateUseOfCode(string code)
        {
            var vefiryCode = _context.VerifyCode.FirstOrDefault(x => x.Code == code);
            if (vefiryCode != null) {
                vefiryCode.IsUsed = true;
                _context.VerifyCode.Update(vefiryCode);
            }
            return Task.CompletedTask;
        }
    }
}
