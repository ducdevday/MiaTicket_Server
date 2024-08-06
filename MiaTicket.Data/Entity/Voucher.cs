using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Voucher
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TotalLimit { get; set; }
        public int? MinQuanityPerOrder { get; set; }
        public int? MaxQuanityPerOrder { get; set; }
        public bool IsPercentage { get; set; }
        public VoucherPercentage? VoucherPercentage { get; set; }
        public VoucherFixedAmount? VoucherFixedAmount { get; set; }
        public Event Event { get; set; }
        public string EventId { get; set; }
    }
}
