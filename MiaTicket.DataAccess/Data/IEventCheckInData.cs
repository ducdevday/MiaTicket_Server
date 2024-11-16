using MiaTicket.Data;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.DataAccess.Data
{
    public interface IEventCheckInData
    {
        public Task<EventCheckIn?> GetEventCheckIn(string code);
        public Task<EventCheckIn> UpdateEventCheckIn(EventCheckIn eventCheckIn);
    }

    public class EventCheckInData : IEventCheckInData
    {
        private readonly MiaTicketDBContext _context;

        public EventCheckInData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<EventCheckIn?> GetEventCheckIn(string code)
        {
            var evtC = _context.EventCheckIn.Include(x => x.Order).ThenInclude(x =>x.OrderTickets).FirstOrDefault(x => x.Order.QrCode == code);
            return Task.FromResult(evtC);
        }

        public Task<EventCheckIn> UpdateEventCheckIn(EventCheckIn eventCheckIn)
        {
            var evtC = _context.EventCheckIn.Update(eventCheckIn);
            return Task.FromResult(evtC.Entity);
        }
    }
}
