using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class AdminLoginValidation : BaseValidation<AdminLoginRequest>
    {
        public AdminLoginValidation(AdminLoginRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Account) || string.IsNullOrEmpty(_value.Password) || !RegexUtil.isPasswordValid(_value.Password)) {
                _message = "Invalid Input";
            }
        }
    }
}
