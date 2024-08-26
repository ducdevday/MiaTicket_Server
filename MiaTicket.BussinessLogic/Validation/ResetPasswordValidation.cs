using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class ResetPasswordValidation : BaseValidation<ResetPasswordRequest>
    {
        public ResetPasswordValidation(ResetPasswordRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (
                string.IsNullOrEmpty(_value.Email)
                || !RegexUtil.isEmailValid(_value.Email)
                || !RegexUtil.isEmailValid(_value.Email)
                || string.IsNullOrEmpty(_value.Code)
                || !RegexUtil.isStringLengthValid(_value.Code, 32)
                || string.IsNullOrEmpty(_value.NewPassword)
                || !RegexUtil.isPasswordValid(_value.NewPassword)
                || !RegexUtil.isStringLengthValid(_value.NewPassword, 255)
                || string.IsNullOrEmpty(_value.ConfirmPassword)
                || !RegexUtil.isPasswordValid(_value.ConfirmPassword)
                || !RegexUtil.isStringLengthValid(_value.ConfirmPassword, 255)
                || _value.NewPassword != _value.ConfirmPassword
                )
            {
                _message = "Invalid Request";
            }
            return;
        }
    }
}
