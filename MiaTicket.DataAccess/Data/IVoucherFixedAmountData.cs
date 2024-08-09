using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IVoucherFixedAmountData
    {

    }

    public class VoucherFixedAmountData : IVoucherFixedAmountData
    {
        private readonly MiaTicketDBContext _context;

        public VoucherFixedAmountData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
