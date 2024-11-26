using Azure.Core;
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
    public interface IOrderData
    {
        Task<List<Event>> GetTrendingEvent(int count);
        Task<Order> CreateOrder(Order order);
        Task<Order?> GetOrder(Guid userId, int orderId);
        Task<Order?> GetOrderById(int orderId);
        Task<List<Order>> GetOrders(Guid userId, int pageIndex, int pageSize, string keyword, OrderStatus orderStatus, out int totalPages);
        Task<Order> UpdateOrder(Order order);
        Task<List<Order>> GetCheckInReportByShowTime(int eventId, int showTimeId);
        Task<List<Order>> GetOrderReport(int eventId, int showTimeId, int pageIndex, int pageSize, out int totalPages);
        Task<List<Order>> GetAllOrderReport(int eventId, int showTimeId);
        Task<ShowTime?> GetOrderSummaryRevenue(int eventId, int showTimeId);
        Task<List<Order>> GetOrderSummaryFigure(int eventId, int showTimeId, DateTime startTime, DateTime endTime);
    }
    public class OrderData : IOrderData
    {
        private readonly MiaTicketDBContext _context;
        public OrderData(MiaTicketDBContext context)
        {
            _context = context;
        }
        public Task<Order> CreateOrder(Order order)
        {
            return Task.FromResult(_context.Order.Add(order).Entity);
        }

        public Task<Order?> GetOrder(Guid userId, int orderId)
        {
            var order = _context.Order.Include(x => x.Event).Include(x => x.OrderTickets).FirstOrDefault(x => x.Id == orderId && x.UserId == userId);
            return Task.FromResult(order);
        }

        public Task<Order?> GetOrderById(int orderId)
        {
            var order = _context.Order.Find(orderId);
            return Task.FromResult(order);
        }

        public Task<List<Order>> GetOrders(Guid userId, int pageIndex, int pageSize, string keyword, OrderStatus orderStatus, out int totalPages)
        {
            var query = _context.Order.Include(x => x.Event).Include(x => x.OrderTickets).Include(x =>x.Payment).Where(x => x.Event.Name.Contains(keyword) && x.OrderStatus == orderStatus && x.UserId == userId).OrderByDescending(x => x.CreatedAt);
            totalPages = query.Count();

            var orders = query.Skip(pageIndex - 1).Take(pageSize).ToList();
            return Task.FromResult(orders);
        }

        public Task<List<Order>> GetOrderReport(int eventId, int showTimeId, int pageIndex, int pageSize, out int totalPages)
        {
            var query = _context.Order.Include(x => x.Event)
                                      .Include(x => x.OrderTickets)
                                      .Include(x => x.Payment)
                                      .Where(x => x.EventId == eventId && x.ShowTimeId == showTimeId);
            totalPages = query.Count();
            var orders = query.Skip(pageIndex - 1).Take(pageSize).ToList();
            return Task.FromResult(orders);
        }
        public Task<List<Order>> GetAllOrderReport(int eventId, int showTimeId)
        {
            var orders = _context.Order.Include(x => x.Event)
                                       .Include(x => x.OrderTickets)
                                       .Include(x => x.Payment)
                                       .Where(x => x.EventId == eventId && x.ShowTimeId == showTimeId).ToList();
            return Task.FromResult(orders);
        }
        public Task<List<Event>> GetTrendingEvent(int count)
        {
            var evts = _context.Order
                               .Include(o => o.Event.ShowTimes)
                               .ThenInclude(o => o.Tickets)
                               .GroupBy(o => o.EventId)
                               .OrderByDescending(g => g.Count())
                               .Select(g => g.First().Event)
                               .Take(count)
                               .ToList();

            return Task.FromResult(evts);
        }

        public Task<Order> UpdateOrder(Order order)
        {
            return Task.FromResult(_context.Order.Update(order).Entity);
        }

        public Task<List<Order>> GetCheckInReportByShowTime(int eventId, int showTimeId)
        {
            var orders = _context.Order.Include(o => o.Payment)
                                          .Include(o => o.EventCheckIn)
                                          .Include(o => o.OrderTickets)
                                             .Where(o => o.EventId == eventId && o.ShowTimeId == showTimeId).ToList();


            return Task.FromResult(orders);
        }

        public Task<ShowTime?> GetOrderSummaryRevenue(int eventId, int showTimeId)
        {
            var showTime = _context.ShowTime
                                        .Include(st => st.Event)
                                        .Include(st => st.Tickets)
                                        .Include(st => st.Orders).ThenInclude(o => o.Payment)
                                        .Include(st => st.Orders).ThenInclude(o => o.OrderTickets)
                                        .FirstOrDefault(o => o.Id == showTimeId && o.EventId == eventId);
            return Task.FromResult(showTime);
        }

        public Task<List<Order>> GetOrderSummaryFigure(int eventId, int showTimeId, DateTime startTime, DateTime endTime)
        {
            var orders = _context.Order.Include(o => o.Payment).Include(o => o.OrderTickets).Where(o => o.EventId == eventId && o.ShowTimeId == showTimeId && o.CreatedAt >= startTime && o.CreatedAt <= endTime).ToList();
            return Task.FromResult(orders);
        }
    }
}
