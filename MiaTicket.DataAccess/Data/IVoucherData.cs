using MiaTicket.Data;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
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
        Task<List<Voucher>> GetVouchers(Func<Voucher, bool> predicate);
        Task<Voucher?> GetVoucherById(int voucherId);
        Task<bool> IsVoucherCodeExist(string code);
        Task<Voucher> UpdateVoucher(Voucher voucher);
        Task DeleteVoucher(Voucher voucher);
        Task<Voucher?> SearchVoucher(int eventId, string code);
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

        public Task<List<Voucher>> GetVouchers(Func<Voucher, bool> predicate)
        {
            return Task.FromResult(_context.Voucher.Include(x => x.Event).Where(predicate).ToList());
        }

        public Task<Voucher?> SearchVoucher(int eventId, string code)
        {
            return Task.FromResult(_context.Voucher.FirstOrDefault(x => x.EventId == eventId && x.Code == code));
        }

        public Task<Voucher> UpdateVoucher(Voucher voucher)
        {
            return Task.FromResult(_context.Voucher.Update(voucher).Entity);
        }
    }
}
