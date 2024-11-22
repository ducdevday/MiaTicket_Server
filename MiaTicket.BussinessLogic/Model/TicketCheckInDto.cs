namespace MiaTicket.BussinessLogic.Model
{
    public class TicketCheckInDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double TicketCheckedInPercentage { get; set; }
        public int TotalCheckedInTicket { get; set; }
        public int TotalPaidTicket { get; set; }
    }
}
