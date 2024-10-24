using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class UseVerificationCodeValidation : BaseValidation<UseVeriicationCodeRequest>
    {
        public UseVerificationCodeValidation(UseVeriicationCodeRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (
                string.IsNullOrEmpty(_value.Email)
                || !RegexUtil.isEmailValid(_value.Email)
                || !RegexUtil.isEmailValid(_value.Email)
                || string.IsNullOrEmpty(_value.Code)
                || !RegexUtil.isStringLengthValid(_value.Code, 32))
            {
                _message = "Invalid Request";
            }
            return;
        }
    }
}
