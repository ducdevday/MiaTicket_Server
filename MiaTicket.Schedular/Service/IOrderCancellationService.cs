using Hangfire;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Setting;

namespace MiaTicket.Schedular.Service
{
    public interface IOrderCancellationService
    {
        public Task ScheduleCancelOrderIfNotPaid(int orderId);
    }

    public class OrderCancellationService : IOrderCancellationService {

        private readonly IDataAccessFacade _context;

        public OrderCancellationService(IDataAccessFacade context)
        {
            _context = context;
        }

        public Task ScheduleCancelOrderIfNotPaid(int orderId)
        {
            BackgroundJob.Schedule(() => CancelOrderIfNotPaid(orderId), TimeSpan.FromMinutes(AppConstant.PAYMENT_LINK_EXPIRE_IN_MINUTES));
            return Task.CompletedTask;
        }

        public async Task CancelOrderIfNotPaid(int orderId)
        {
            var order = await _context.OrderData.GetOrderById(orderId);
            if (order != null && order.OrderStatus == OrderStatus.Pending)
            {
                order.OrderStatus = OrderStatus.Canceled;
                await _context.OrderData.UpdateOrder(order);
                await _context.Commit();
            }
        }
    }
}
