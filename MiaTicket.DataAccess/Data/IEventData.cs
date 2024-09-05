using MiaTicket.Data;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess.Model;
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
        Task DeleteEvent(Event entity);
        Task<GetEventsResult> GetEvents(Guid userId, string key, int page, int size);
        Task<List<Event>> GetLatestEvent(int count);
        Task<List<Event>> GetEventsByCategory(int categoryId, int count);
        Task<bool> IsEventSortTypeValid(int type);
        Task<GetEventsResult> SearchEvent(string keyword, int page, int size, string location, List<int> categoriesId, List<double> priceRanges, EventSortType sortBy);
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

        public Task<Event?> GetEventById(int id)
        {
            var evt = _context.Event.Include(e => e.ShowTimes)
                .ThenInclude(st => st.Tickets).FirstOrDefault(x => x.Id == id);
            return Task.FromResult(evt);
        }

        public Task<GetEventsResult> GetEvents(Guid userId, string key, int page, int size)
        {
            var query = _context.Event.Include(e => e.ShowTimes).Where(x => x.UserId == userId && x.Name.Contains(key));
            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / size);

            var evts = query.Skip((page - 1) * size).Take(size).ToList();

            var result = new GetEventsResult
            {
                Items = evts,
                Pagination = new PaginationResult()
                {
                    CurrentPage = page,
                    CurrentSize = size,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords,
                }
            };
            return Task.FromResult(result);
        }

        public Task<List<Event>> GetEventsByCategory(int categoryId, int count)
        {
            var evts = _context.Event.Include(e => e.ShowTimes)
                    .ThenInclude(st => st.Tickets).Where(x => x.CategoryId == categoryId).Take(count).ToList();
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

        public Task<GetEventsResult> SearchEvent(string keyword, int page, int size, string location, List<int> categoriesId, List<double> priceRanges, EventSortType sortBy)
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
                case EventSortType.Lastest:
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

            var result = new GetEventsResult
            {
                Items = evts,
                Pagination = new PaginationResult()
                {
                    CurrentPage = page,
                    CurrentSize = size,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords,
                }
            };
            return Task.FromResult(result);
        }

        public Task<Event> UpdateEvent(Event entity)
        {
            return Task.FromResult(_context.Event.Update(entity).Entity);
        }
    }
}
