using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IEventOrganizerData
    {
        Task<List<Event>> GetEventsOfUser(Guid userId, string key, int status, int page, int size, out int count);
        Task<EventOrganizer?> GetEventById(int eventId, Guid organizerId);
    }

    public class EventOrganizerData : IEventOrganizerData
    {
        private MiaTicketDBContext _context;

        public EventOrganizerData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<EventOrganizer?> GetEventById(int eventId, Guid organizerId)
        {
            var eventOrganizer = _context.EventOrganizer.Find(eventId, organizerId);
            return Task.FromResult(eventOrganizer);
        }

        public Task<List<Event>> GetEventsOfUser(Guid userId, string key, int status, int page, int size, out int count)
        {
            var query = _context.EventOrganizer.Where(x => x.OrganizerId == userId)
                                                .Include(x => x.Event)
                                                .ThenInclude(x => x.ShowTimes)
                                                .Select(x => x.Event)
                                                .Where(e => e.Name.Contains(key) && e.Status == (EventStatus)status).OrderByDescending(x => x.CreatedAt);
            count = query.Count();
            var evts = query.Skip((page - 1) * size).Take(size).ToList();
            return Task.FromResult(evts);
        }
    }
}
