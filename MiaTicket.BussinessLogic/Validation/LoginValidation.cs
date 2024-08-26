using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class LoginValidation : BaseValidation<LoginRequest>
    {
        public LoginValidation(LoginRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (
                string.IsNullOrEmpty(_value.Email)
                || !RegexUtil.isEmailValid(_value.Email)
                || string.IsNullOrEmpty(_value.Password)
                || !RegexUtil.isPasswordValid(_value.Password)
                || !RegexUtil.isStringLengthValid(_value.Password, 255)
                )
            {
                _message = "Invalid Request";
            }
            return;
        }
    }
}
