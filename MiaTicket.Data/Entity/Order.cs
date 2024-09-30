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
        public string EventName { get; set; }
        public string AddressName { get; set; }
        public string AddressDetail { get; set; }
        public string BackgroundUrl { get; set; }
        public string LogoUrl { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerInformation { get; set; }
        public string OrganizerLogoUrl { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double TotalPrice { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string CreatedAt { get; set; }
        public string QrCode { get; set; }
        public bool IsUsed { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
        public ShowTime ShowTime { get; set; }
        public int ShowTimeId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderTicket> OrderTickets { get; set; }
        public VnPayInformation? VnPayInformation { get; set; }
        public ZaloPayInformation? ZaloPayInformation { get;set; }
    }
}
