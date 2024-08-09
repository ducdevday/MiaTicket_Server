using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IEventData
    {
    }

    public class EventData : IEventData{
        private readonly MiaTicketDBContext _context;

        public EventData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
