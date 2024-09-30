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
    public interface IVNPayInformationData
    {
        Task<VnPayInformation?> GetVnPayInformation(Func<VnPayInformation, bool> predicate);
        Task<VnPayInformation> UpdateVnPayInformation(VnPayInformation vnPayInformation);
    }

    public class VNPayInformationData : IVNPayInformationData
    {
        private readonly MiaTicketDBContext _context;
        public VNPayInformationData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<VnPayInformation?> GetVnPayInformation(Func<VnPayInformation, bool> predicate)
        {
            return Task.FromResult(_context.VnPayInformation.Include(x => x.Order).ThenInclude(x => x.ShowTime)
                                                            .Include(x => x.Order).ThenInclude(x => x.OrderTickets)
                                                            .FirstOrDefault(predicate));
        }

        public Task<VnPayInformation> UpdateVnPayInformation(VnPayInformation vnPayInformation)
        {
            return Task.FromResult(_context.VnPayInformation.Update(vnPayInformation).Entity);
        }
    }
}
