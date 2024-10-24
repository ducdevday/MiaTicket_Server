using MiaTicket.Data;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IVoucherData
    {
        Task<Voucher> CreateVoucher(Voucher voucher);
        Task<List<Voucher>> GetVoucherByEventId(int eventId);
        Task<List<Voucher>> SearchVouchers(int eventId, string keyword, out string eventName);
        Task<Voucher?> GetVoucherById(int voucherId);
        Task<bool> IsVoucherCodeExist(string code);
        Task<Voucher> UpdateVoucher(Voucher voucher);
        Task DeleteVoucher(Voucher voucher);
        Task<Voucher?> FindVoucher(int eventId, string code);
    }

    public class VoucherData : IVoucherData
    {
        private readonly MiaTicketDBContext _context;

        public VoucherData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Voucher> CreateVoucher(Voucher voucher)
        {
            return Task.FromResult(_context.Voucher.Add(voucher).Entity);
        }

        public Task DeleteVoucher(Voucher voucher)
        {
            return Task.FromResult(_context.Voucher.Remove(voucher));
        }

        public Task<bool> IsVoucherCodeExist(string code)
        {
            return Task.FromResult(_context.Voucher.Any(x => x.Code == code));

        }

        public Task<Voucher?> GetVoucherById(int voucherId)
        {
            return Task.FromResult(_context.Voucher.Include(x => x.Event).FirstOrDefault(v => v.Id == voucherId));
        }

        public Task<List<Voucher>> SearchVouchers(int eventId, string keyword, out string eventName)
        {
            var vouchers = _context.Voucher
                       .Include(x => x.Event)
                       .Where(x => x.EventId == eventId && x.Name.Contains(keyword))
                       .ToList();

            eventName = vouchers.FirstOrDefault()?.Event?.Name ?? string.Empty; 

            return Task.FromResult(vouchers);
        }

        public Task<Voucher?> FindVoucher(int eventId, string code)
        {
            return Task.FromResult(_context.Voucher.FirstOrDefault(x => x.EventId == eventId && x.Code == code));
        }

        public Task<Voucher> UpdateVoucher(Voucher voucher)
        {
            return Task.FromResult(_context.Voucher.Update(voucher).Entity);
        }

        public Task<List<Voucher>> GetVoucherByEventId(int eventId)
        {
            return Task.FromResult(_context.Voucher.Where(x => x.EventId == eventId).ToList());
        }
    }
}
