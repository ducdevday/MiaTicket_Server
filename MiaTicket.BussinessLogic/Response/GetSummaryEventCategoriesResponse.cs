using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetSummaryEventCategoriesResponse : BaseApiResponse<List<EventCategoryFigureDto>>
    {
        public GetSummaryEventCategoriesResponse(HttpStatusCode statusCode, string message, List<EventCategoryFigureDto> data) : base(statusCode, message, data)
        {
        }
    }
}
