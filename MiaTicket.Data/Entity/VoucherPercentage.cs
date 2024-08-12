using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class VoucherPercentage
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public Voucher Voucher { get; set; }
        public int VoucherId { get; set; }
    }
}
