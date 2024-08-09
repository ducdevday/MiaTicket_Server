using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IShowTimeData
    {
    }

    public class ShowTimeData : IShowTimeData    {
        private readonly MiaTicketDBContext _context;

        public ShowTimeData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
