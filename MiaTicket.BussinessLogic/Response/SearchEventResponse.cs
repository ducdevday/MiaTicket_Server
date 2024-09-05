using MiaTicket.BussinessLogic.Model;
using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class SearchEventResponse : BaseApiResponse<SearchEventDataResponse>
    {
        public SearchEventResponse(HttpStatusCode statusCode, string message, SearchEventDataResponse data) : base(statusCode, message, data)
        {
        }
    }
}
