using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class UpdateAccountValidation : BaseValidation<UpdateAccountRequest>
    {
        public UpdateAccountValidation(UpdateAccountRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Name)
                            || !RegexUtil.isStringLengthValid(_value.Name, 255)
                            || _value.BirthDate > DateTime.UtcNow
                            || !RegexUtil.isPhomeNumberValid(_value.PhoneNumber)
                            )
            {
                _message = "Invalid Request";
            }
            return;
        }
    }
}
