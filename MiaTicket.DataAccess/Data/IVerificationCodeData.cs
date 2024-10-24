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
    public interface IVerificationCodeData
    {
        public Task CreateVerificationCode(VerificationCode verificationCode);
        public Task<bool> IsValidCode(Guid uid,string code, VerificationType type);
        public Task<bool> IsUsedCode(string code);
        public Task UpdateUseOfCode(string code);
    }

    public class VerificationCodeData : IVerificationCodeData
    {
        private readonly MiaTicketDBContext _context;

        public VerificationCodeData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task CreateVerificationCode(VerificationCode verificationCode)
        {
            _context.VerificationCode.Add(verificationCode);
            return Task.CompletedTask;
        }

        public Task<bool> IsUsedCode(string code)
        {
            var verifyCode = _context.VerificationCode.FirstOrDefault((x) => x.Code == code);
            if (verifyCode == null || verifyCode.IsUsed == false) {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> IsValidCode(Guid uid, string code, VerificationType type)
        {
            var validCode = _context.VerificationCode.OrderBy(x => x.Id).LastOrDefault(x => x.UserId == uid && x.Type == type);
            if (validCode != null && validCode.Code == code && validCode.ExpireAt > DateTime.Now) {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task UpdateUseOfCode(string code)
        {
            var vefiryCode = _context.VerificationCode.FirstOrDefault(x => x.Code == code);
            if (vefiryCode != null) {
                vefiryCode.IsUsed = true;
                _context.VerificationCode.Update(vefiryCode);
            }
            return Task.CompletedTask;
        }
    }
}
