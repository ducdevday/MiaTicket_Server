using MiaTicket.BussinessLogic.Response;
using System.Net;

namespace MiaTicket.BussinessLogic.Model
{
    public class SearchEventDataResponse : BaseApiResponse<SearchEventDto>
    {
        public SearchEventDataResponse(HttpStatusCode statusCode, string message, SearchEventDto data) : base(statusCode, message, data)
        {
        }
    }
}
