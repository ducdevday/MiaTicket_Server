using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class UpdateVoucherRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventId { get; set; }
        public VoucherType Type { get; set; }
        public double Value { get; set; }
        public int? Quantity { get; set; }
        public int? MinQuantityPerOrder { get; set; }
        public int? MaxQuantityPerOrder { get; set; }
    }
}
