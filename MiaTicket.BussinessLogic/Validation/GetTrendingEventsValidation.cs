using MiaTicket.BussinessLogic.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class GetTrendingEventsValidation : BaseValidation<GetTrendingEventsRequest>
    {
        public GetTrendingEventsValidation(GetTrendingEventsRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (_value.Count < 0) { 
                _message = "Invalid Request";
            }
        }
    }
}
