using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class GetMyOrdersValidation : BaseValidation<GetMyOrdersRequest>
    {
        public GetMyOrdersValidation(GetMyOrdersRequest input) : base(input)
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
