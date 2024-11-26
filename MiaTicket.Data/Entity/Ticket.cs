using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int InitQuantity { get; set; }
        public int MinimumPurchase { get; set; }
        public int MaximumPurchase { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ShowTime ShowTime { get; set; }
        public int ShowTimeId { get; set; }
        public List<OrderTicket>? OrderTickets {get;set;}

    }
}
