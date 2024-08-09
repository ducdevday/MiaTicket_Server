using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IVoucherData
    {
    }

    public class VoucherData : IVoucherData
    {
        private readonly MiaTicketDBContext _context;

        public VoucherData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
