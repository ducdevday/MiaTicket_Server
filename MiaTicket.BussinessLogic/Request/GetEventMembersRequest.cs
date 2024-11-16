using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class GetEventMembersRequest : BaseApiRequest
    {
        private string _keyword;
        public string Keyword
        {
            get => _keyword ?? string.Empty;
            set => _keyword = value;
        }
    }
}
