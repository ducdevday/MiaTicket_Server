using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IVoucherPercentage
    {

    }

    public class VoucherPercentage: IVoucherPercentage {
        private readonly MiaTicketDBContext _context;

        public VoucherPercentage(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
