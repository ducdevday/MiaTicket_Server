using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.DataAccess.Data
{
    public interface IEventOrganizerData
    {
        Task<List<Event>> GetEventsOfUser(Guid userId, string key, int status, int page, int size, out int count);
        Task<EventOrganizer?> GetEventOrganizerById(int eventId, Guid organizerId);
        Task<List<EventOrganizer>> GetEventMembers(int eventId, string key, int page, int size, out int count);
        Task<EventOrganizer> UpdateEventMember(EventOrganizer eventOrganizer);
        Task DeleteEventMember(EventOrganizer eventOrganizer);
        Task AddEventMember(EventOrganizer eventOrganizer);
        Task<bool> IsMemberExist(int eventId, string organizerEmail);
    }

    public class EventOrganizerData : IEventOrganizerData
    {
        private MiaTicketDBContext _context;

        public EventOrganizerData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<EventOrganizer?> GetEventOrganizerById(int eventId, Guid organizerId)
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

        public Task<List<EventOrganizer>> GetEventMembers(int eventId, string key, int page, int size, out int count)
        {
            var query = _context.EventOrganizer.Where(x => x.EventId == eventId)
                                               .Include(x => x.Organizer)
                                               .Where(x => x.Organizer.Name.Contains(key));
            count = query.Count();
            var eventMembers = query.Skip((page - 1) * size).Take(size).ToList();
            return Task.FromResult(eventMembers);

        }

        public Task<EventOrganizer> UpdateEventMember(EventOrganizer eventOrganizer)
        {
            var eo = _context.EventOrganizer.Update(eventOrganizer);
            return Task.FromResult(eo.Entity);
        }

        public Task DeleteEventMember(EventOrganizer eventOrganizer)
        {
            _context.EventOrganizer.Remove(eventOrganizer);
            return Task.CompletedTask;
        }

        public Task AddEventMember(EventOrganizer eventOrganizer)
        {
            _context.EventOrganizer.Add(eventOrganizer);
            return Task.CompletedTask;
        }

        public Task<bool> IsMemberExist(int eventId, string organizerEmail)
        {
            var isMemberExist = _context.EventOrganizer.Include(x => x.Organizer).Any(x => x.EventId == eventId && x.Organizer.Email == organizerEmail);
            return Task.FromResult(isMemberExist);
        }
    }
}
