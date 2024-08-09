using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface ICategoryData
    {

    }

    public class CategoryData: ICategoryData {
        private readonly MiaTicketDBContext _context;

        public CategoryData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
