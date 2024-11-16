using MiaTicket.BussinessLogic.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class GetEventMembersValidation : BaseValidation<GetEventMembersRequest>
    {
        public GetEventMembersValidation(GetEventMembersRequest input) : base(input)
        {

        }

        public override void Validate()
        {
            if (_value.PageIndex < 1 || _value.PageSize < 1)
            {
                _message = "Invalid Request";
            }
        }
    }
}
