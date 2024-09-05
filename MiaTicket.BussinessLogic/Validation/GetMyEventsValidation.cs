using MiaTicket.BussinessLogic.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class GetMyEventsValidation : BaseValidation<GetMyEventsRequest>
    {
        public GetMyEventsValidation(GetMyEventsRequest input) : base(input)
        {

        }

        public override void Validate()
        {
            if(_value.Page < 1 || _value.Size < 1)
            {
                _message = "Invalid Request";
            }
        }
    }
}
