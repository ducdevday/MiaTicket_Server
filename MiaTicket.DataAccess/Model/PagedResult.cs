using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Model
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }

        public PaginationResult Pagination { get; set; }

    }
}
