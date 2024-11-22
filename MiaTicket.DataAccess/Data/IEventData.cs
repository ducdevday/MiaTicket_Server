using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.DataAccess.Data
{
    public interface IEventData
    {
        Task<Event> CreateEvent(Event entity);
        Task<string?> GetEventName(int eventId);
        Task<List<ShowTime>?> GetEventShowTimes(int eventId);
        Task<Event?> GetEventById(int id);
        Task<Event?> GetEventBooking(int eventId, int showTimeId);
        Task<Event?> GetEventBooking(int eventId, int showTimeId, List<int> ticketIds);
        Task<bool> IsExistEvent(int id);
        Task<Event> UpdateEvent(Event entity);
        Task DeleteEvent(Event entity);
        Task<List<Event>> GetLatestEvent(int count);
        Task<List<Event>> GetEventsByCategory(int categoryId, int count);
        Task<bool> IsEventSortTypeValid(int type);
        Task<List<Event>> SearchEvent(string keyword, int page, int size, string location, List<int> categoriesId, List<double> priceRanges, EventSortType sortBy);
        Task<List<Order>> GetCheckInReportByShowTime(int eventId, int showTimeId);
    }

    public class EventData : IEventData
    {
        private readonly MiaTicketDBContext _context;

        public EventData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Event> CreateEvent(Event entity)
        {
            return Task.FromResult(_context.Event.Add(entity).Entity);
        }

        public Task DeleteEvent(Event entity)
        {
            _context.Event.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<string?> GetEventName(int eventId) {
            var evtName = _context.Event.Where(x => x.Id == eventId).Select(x =>x.Name).FirstOrDefault();
            return Task.FromResult(evtName);
        }

        public Task<List<ShowTime>?> GetEventShowTimes(int eventId)
        {
            var showTimes = _context.Event.Include(x => x.ShowTimes).Where(x => x.Id == eventId).Select(x => x.ShowTimes).FirstOrDefault();
            return Task.FromResult(showTimes);
        }

        public Task<Event?> GetEventById(int id)
        {
            var evt = _context.Event.Include(e => e.BankAccount).Include(e => e.ShowTimes)
                .ThenInclude(st => st.Tickets).FirstOrDefault(x => x.Id == id);
            return Task.FromResult(evt);
        }

        public Task<Event?> GetEventBooking(int eventId, int showTimeId)
        {
            var evt = _context.Event.Include(e => e.ShowTimes.Where(x =>x.Id == showTimeId))
                                    .ThenInclude(st => st.Tickets)
                                    .FirstOrDefault(e => e.Id == eventId && e.ShowTimes.Any(st => st.Id == showTimeId));
            return Task.FromResult(evt);
        }

        public Task<Event?> GetEventBooking(int eventId, int showTimeId, List<int> ticketIds)
        {
            var evt = _context.Event
                .Include(e => e.ShowTimes.Where(e => e.Id == showTimeId))             
                .ThenInclude(st => st.Tickets.Where(t => ticketIds.Contains(t.Id)))               
                .FirstOrDefault(e => e.Id == eventId && e.ShowTimes.Any(st => st.Id == showTimeId && ticketIds.All(x => st.Tickets.Select(t =>t.Id).Contains(x))));
            return Task.FromResult(evt);

        }

        public Task<List<Event>> GetEventsByCategory(int categoryId, int count)
        {
            var evts = _context.Event
                .Include(e => e.ShowTimes)
                .ThenInclude(st => st.Tickets)
                .Where(x => x.CategoryId == categoryId)
                .Take(count)
                .ToList();

            return Task.FromResult(evts);
        }

        public Task<List<Event>> GetLatestEvent(int count)
        {
            var evts = _context.Event.OrderByDescending(x => x.CreatedAt).Take(count).ToList();
            return Task.FromResult(evts);
        }

        public Task<bool> IsEventSortTypeValid(int sortType)
        {
            bool isEventSortTypeValid = Enum.IsDefined(typeof(EventSortType), sortType);
            return Task.FromResult(isEventSortTypeValid);
        }

        public Task<bool> IsExistEvent(int id)
        {
            var evt = _context.Event.Find(id);
            return Task.FromResult(evt == null ? false : true);
        }

        public Task<List<Event>> SearchEvent(string keyword, int page, int size, string location, List<int> categoriesId, List<double> priceRanges, EventSortType sortBy)
        {
            var query = _context.Event
                .Include(e => e.ShowTimes)
                    .ThenInclude(st => st.Tickets).AsQueryable();

            query = query.Where(x =>
                (string.IsNullOrEmpty(keyword) || x.Name.Contains(keyword))
                && (string.IsNullOrEmpty(location) || x.AddressProvince == location)
                && (categoriesId.Count == 0 || categoriesId.Contains(x.CategoryId))
            );

            if (priceRanges.Count == 2)
            {
                query = query.Where(e => e.ShowTimes.Any(st => st.Tickets.Any(t => t.Price >= priceRanges[0] && t.Price <= priceRanges[1])));
            }

            switch (sortBy)
            {
                case EventSortType.Latest:
                    query = query.OrderByDescending(e => e.CreatedAt);
                    break;
                case EventSortType.PriceLowToHigh:
                    query = query.OrderBy(e => e.ShowTimes.SelectMany(st => st.Tickets).Min(t => t.Price));
                    break;
                case EventSortType.PriceHighToLow:
                    query = query.OrderByDescending(e => e.ShowTimes.SelectMany(st => st.Tickets).Max(t => t.Price));
                    break;
            }

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / size);
            var evts = query.Skip((page - 1) * size).Take(size).ToList();

            return Task.FromResult(evts);
        }

        public Task<Event> UpdateEvent(Event entity)
        {
            return Task.FromResult(_context.Event.Update(entity).Entity);
        }

        public Task<List<Order>> GetCheckInReportByShowTime(int eventId, int showTimeId)
        {
            var orders = _context.Order.Include(o => o.Payment)
                                          .Include(o => o.EventCheckIn)
                                          .Include(o => o.OrderTickets)
                                             .Where(o => o.EventId == eventId && o.ShowTimeId == showTimeId).ToList();


            return Task.FromResult(orders);
        }
    }
}
