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
            var order = _context.Order.Include(x => x.OrderTickets).FirstOrDefault(x => x.Id == orderId && x.UserId == userId);
            return Task.FromResult(order);
        }

        public Task<Order?> GetOrderById(int orderId)
        {
            var order = _context.Order.Find(orderId);
            return Task.FromResult(order);
        }

        public Task<List<Order>> GetOrders(Guid userId, int pageIndex, int pageSize, string keyword, OrderStatus orderStatus, out int totalPages)
        {
            var query = _context.Order.Include(x => x.OrderTickets).Include(x =>x.VnPayInformation).Include(x => x.ZaloPayInformation).Where(x => x.EventName.Contains(keyword) && x.OrderStatus == orderStatus && x.UserId == userId).OrderByDescending(x => x.CreatedAt);
            totalPages = query.Count();

            var orders = query.Skip(pageIndex - 1).Take(pageSize).ToList();
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
    }
}
