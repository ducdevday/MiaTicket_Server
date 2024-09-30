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
    public interface IZaloPayInformationData
    {
        Task<ZaloPayInformation?> GetZaloPayInformation(Func<ZaloPayInformation, bool> predicate);
        Task<ZaloPayInformation> UpdateZaloPayInformation(ZaloPayInformation zaloPayInformation);
    }

    public class ZaloPayInformationData : IZaloPayInformationData
    {
        private readonly MiaTicketDBContext _context;

        public ZaloPayInformationData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<ZaloPayInformation?> GetZaloPayInformation(Func<ZaloPayInformation, bool> predicate)
        {
            return Task.FromResult(_context.ZaloPayInformation.Include(x => x.Order).ThenInclude(x => x.ShowTime)
                                                             .Include(x => x.Order).ThenInclude(x => x.OrderTickets)
                                                             .FirstOrDefault(predicate));
        }

        public Task<ZaloPayInformation> UpdateZaloPayInformation(ZaloPayInformation zaloPayInformation)
        {
            return Task.FromResult(_context.ZaloPayInformation.Update(zaloPayInformation).Entity);

        }
    }
}
