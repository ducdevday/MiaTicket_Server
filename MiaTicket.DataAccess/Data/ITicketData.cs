using MiaTicket.Data;
using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface ITicketData
    {
        Task<Ticket> UpdateTicket(Ticket ticket);
        Task<List<Ticket>> GetTickets(Func<Ticket, bool> predicate);
    }

    public class TicketData : ITicketData
    {
        public MiaTicketDBContext _context;

        public TicketData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Ticket> UpdateTicket(Ticket ticket)
        {
            return Task.FromResult(_context.Ticket.Update(ticket).Entity);
        }

        public Task<List<Ticket>> GetTickets(Func<Ticket, bool> predicate)
        {
            return Task.FromResult(_context.Ticket.Where(predicate).ToList());
        }
    }
}
