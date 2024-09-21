using MiaTicket.Data;
using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IShowTimeData
    {
        Task<ShowTime> UpdateShowTime(ShowTime showTime);
    }

    public class ShowTimeData : IShowTimeData
    {
        private readonly MiaTicketDBContext _context;

        public ShowTimeData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<ShowTime> UpdateShowTime(ShowTime showTime)
        {
            return Task.FromResult(_context.ShowTime.Update(showTime).Entity);
        }
    }
}
