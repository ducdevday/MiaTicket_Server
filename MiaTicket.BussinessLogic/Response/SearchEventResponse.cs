using MiaTicket.BussinessLogic.Model;
using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class SearchEventResponse : BaseApiResponse<List<SearchEventDto>>
    {
        public SearchEventResponse(HttpStatusCode statusCode, string message, List<SearchEventDto> data) : base(statusCode, message, data)
        {
        }
    }
}
