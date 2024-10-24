using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string BankNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
