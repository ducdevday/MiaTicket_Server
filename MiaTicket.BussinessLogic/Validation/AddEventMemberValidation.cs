using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class AddEventMemberValidation : BaseValidation<AddEventMemberRequest>
    {
        public AddEventMemberValidation(AddEventMemberRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.MemberEmail) || RegexUtil.isEmailValid(_value.MemberEmail)) {
                _message = "Invalid Input";
            }
        }
    }
}
