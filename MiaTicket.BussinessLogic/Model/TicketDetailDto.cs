using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class TicketDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public int MinimumPurchase { get; set; }
        public int MaximumPurchase { get; set; }
        public string Description { get; set; }
    }
}
