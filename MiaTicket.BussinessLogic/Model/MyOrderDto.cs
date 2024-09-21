using MiaTicket.Data.Entity;

namespace MiaTicket.BussinessLogic.Model
{
    public class MyOrderDto 
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string EventName { get; set; }
        public DateTime ShowTimeStart { get; set; }
        public DateTime ShowTimeEnd { get; set; }
        public string AddressName { get; set; }
        public string AddressDetail { get; set; }
        public List<OrderTicketDetailDto> OrderTickets { get; set; }
        public double TotalPrice { get; set; }
        public bool IsCanCancel { get; set; } = false;
        public bool IsCanRepayment { get; set; } = false ;
        public string PaymentUrl { get; set; } = string.Empty;

    }
}
