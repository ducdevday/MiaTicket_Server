using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IAdminData
    {

    }

    public class AdminData : IAdminData {
        private readonly MiaTicketDBContext _context;

        public AdminData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
