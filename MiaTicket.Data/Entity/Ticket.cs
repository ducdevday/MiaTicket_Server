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
        public int MininumPurchase { get; set; }
        public int MaximumPurchase { get; set; }
        public DateTime SaleStart { get; set; }
        public DateTime SaleEnd { get; set; }
        public DateTime DateStart { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public ShowTime ShowTime { get; set; }
        public int ShowTimeId { get; set; }

    }
}
