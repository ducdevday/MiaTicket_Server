using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class SendVerifyCodeValidation : BaseValidation<SendVerifyCodeRequest>
    {
        public SendVerifyCodeValidation(SendVerifyCodeRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (
               string.IsNullOrEmpty(_value.Email)
               || !RegexUtil.isEmailValid(_value.Email)
               )
            {
                _message = "Invalid Request";
            }
            return;
        }
    }
}
