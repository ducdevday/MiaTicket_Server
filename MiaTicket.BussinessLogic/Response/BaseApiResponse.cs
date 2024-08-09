using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class BaseApiResponse<T>
    {
        public int StatusCode;
        public string Message;
        public T Data;
    }
}
