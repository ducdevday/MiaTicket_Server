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
    public interface IEventData
    {
        Task<Event> CreateEvent(Event entity);
        Task<Event?> GetEventById(int id);
        Task<bool> IsExistEvent(int id);
        Task<Event> UpdateEvent(Event entity);
    }

    public class EventData : IEventData{
        private readonly MiaTicketDBContext _context;

        public EventData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Event> CreateEvent(Event entity)
        {
            return Task.FromResult(_context.Event.Add(entity).Entity);
        }

        public Task<Event?> GetEventById(int id)
        {
            var evt = _context.Event.Include(e => e.ShowTimes)
                .ThenInclude(st => st.Tickets).FirstOrDefault(x => x.Id == id);
            return Task.FromResult(evt);
        }

        public Task<bool> IsExistEvent(int id)
        {
            var evt = _context.Event.Find(id);
            return Task.FromResult(evt == null ? false : true);
        }

        public Task<Event> UpdateEvent(Event entity)
        {
            return Task.FromResult(_context.Event.Update(entity).Entity);
        }
    }
}
