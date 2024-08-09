using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IBannerData
    {

    }


    public class BannerData : IBannerData {
        private readonly MiaTicketDBContext _context;

        public BannerData(MiaTicketDBContext context)
        {
            _context = context;
        }


    }
}
