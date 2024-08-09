using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IOrderData
    {

    }

    public class OrderData : IOrderData
    {
        private readonly MiaTicketDBContext _context;

        public OrderData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
