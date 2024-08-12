using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public abstract class BaseApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        protected BaseApiResponse(HttpStatusCode statusCode, string message, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
