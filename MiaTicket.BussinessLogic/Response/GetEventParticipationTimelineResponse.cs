using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventParticipationTimelineResponse : BaseApiResponse<List<EventParticipationDto>>
    {
        public GetEventParticipationTimelineResponse(HttpStatusCode statusCode, string message, List<EventParticipationDto> data) : base(statusCode, message, data)
        {

        }
    }
}
