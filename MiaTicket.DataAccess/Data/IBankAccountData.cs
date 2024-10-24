using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IBankAccountData
    {

    }

    public class BankAccountData : IBankAccountData
    {

        private readonly MiaTicketDBContext _context;

        public BankAccountData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
