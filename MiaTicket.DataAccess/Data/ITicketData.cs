using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface ITicketData
    {

    }

    public class TicketData : ITicketData
    {
        public MiaTicketDBContext _context;

        public TicketData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
