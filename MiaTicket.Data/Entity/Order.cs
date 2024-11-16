using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string QrCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public EventCheckIn EventCheckIn { get; set; }
        public Payment Payment { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
        public ShowTime ShowTime { get; set; }
        public int ShowTimeId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderTicket> OrderTickets { get; set; }
    }
}
