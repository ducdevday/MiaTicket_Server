using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class VoucherDiscoveryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
        public VoucherType Type { get; set; }
        public bool IsAvailable { get; set; }
    }
}
