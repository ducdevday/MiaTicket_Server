using MiaTicket.Data;
using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IOrderData
    {
        Task<List<Event>> GetTrendingEvent(int count);
    }

    public class OrderData : IOrderData
    {
        private readonly MiaTicketDBContext _context;

        public OrderData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<List<Event>> GetTrendingEvent(int count)
        {
            var evts = _context.Order.GroupBy(o => o.EventId)
                                        .OrderByDescending(g => g.Count())
                                        .Select(g => g.First().Event)
                                        .Take(count)
                                        .ToList();

            return Task.FromResult(evts);
        }
    }
}
