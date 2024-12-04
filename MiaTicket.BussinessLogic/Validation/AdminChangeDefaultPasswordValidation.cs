using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class AdminChangeDefaultPasswordValidation : BaseValidation<AdminChangeDefaultPasswordRequest>
    {
        public AdminChangeDefaultPasswordValidation(AdminChangeDefaultPasswordRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Account) || 
                string.IsNullOrEmpty(_value.DefaultPassword) || !RegexUtil.isPasswordValid(_value.DefaultPassword)
                || string.IsNullOrEmpty(_value.NewPassword) || !RegexUtil.isPasswordValid(_value.NewPassword)
                || string.IsNullOrEmpty(_value.ConfirmNewPassword) || !RegexUtil.isPasswordValid(_value.ConfirmNewPassword)
                || _value.NewPassword != _value.ConfirmNewPassword) {
                _message = "Invalid Input";
            }
        }
    }
}
