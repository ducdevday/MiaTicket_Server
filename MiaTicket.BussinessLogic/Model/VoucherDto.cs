using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class VoucherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public VoucherType Type { get; set; }
        public double Value { get; set; }
        public int? InitQuantity { get; set; }
        public int? AppliedQuantity { get; set; }
        public int? MinQuantityPerOrder { get; set; }
        public int? MaxQuantityPerOrder { get; set; }
    }
}
