using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;

namespace MiaTicket.BussinessLogic.Validation
{
    public class ChangePasswordValidation : BaseValidation<ChangePasswordRequest>
    {
        public ChangePasswordValidation(ChangePasswordRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (
                string.IsNullOrEmpty(_value.CurrentPassword)
                || !RegexUtil.isPasswordValid(_value.CurrentPassword)
                || !RegexUtil.isStringLengthValid(_value.CurrentPassword, 255)
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
