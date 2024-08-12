using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class VoucherFixedAmount
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public Voucher Voucher { get; set; }
        public int VoucherId { get; set; }
    }
}
