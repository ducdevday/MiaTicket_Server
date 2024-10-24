using MiaTicket.Data;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.DataAccess.Data
{
    public interface IPaymentData
    {
        Task<Payment?> GetPayment(Func<Payment, bool> predicate);
        Task<Payment> UpdatePayment(Payment vnPayInformation);
    }


    public class PaymentData : IPaymentData
    {
        private readonly MiaTicketDBContext _context;
        public PaymentData(MiaTicketDBContext context)
        {
            _context = context;
        }

        Task<Payment?> IPaymentData.GetPayment(Func<Payment, bool> predicate)
        {
            return Task.FromResult(_context.Payment.Include(x => x.Order).ThenInclude(x => x.Event)
                                                   .Include(x => x.Order).ThenInclude(x => x.ShowTime)
                                                   .Include(x => x.Order).ThenInclude(x => x.OrderTickets)
                                                   .FirstOrDefault(predicate));
        }

        public Task<Payment> UpdatePayment(Payment payment)
        {
            return Task.FromResult(_context.Payment.Update(payment).Entity);
        }
    }
}
