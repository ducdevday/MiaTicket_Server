using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class GetMyOrdersRequest : BaseApiRequest
    {
        private string _keyword;
        public string Keyword
        {
            get => _keyword ?? string.Empty;
            set => _keyword = value;
        }
        public OrderStatus OrderStatus { get; set; }

    }
}
