using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IVoucherPercentageData
    {

    }

    public class VoucherPercentage: IVoucherPercentageData {
        private readonly MiaTicketDBContext _context;

        public VoucherPercentage(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
