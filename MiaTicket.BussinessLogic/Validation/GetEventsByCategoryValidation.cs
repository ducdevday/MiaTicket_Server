using MiaTicket.BussinessLogic.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class GetEventsByCategoryValidation : BaseValidation<GetEventsByCategoryRequest>
    {
        public GetEventsByCategoryValidation(GetEventsByCategoryRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (_value.Count < 0) {
                _message = "Invalid Request";
            };
        }
    }
}
