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
    public class CreateOrderValidation : BaseValidation<CreateOrderRequest>
    {
        public CreateOrderValidation(CreateOrderRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.ReceiverName)
                            || !RegexUtil.isStringLengthValid(_value.ReceiverName, 255)
                            || string.IsNullOrEmpty(_value.ReceiverEmail)
                            || !RegexUtil.isEmailValid(_value.ReceiverEmail)
                            || string.IsNullOrEmpty(_value.ReceiverPhoneNumber)
                            || !RegexUtil.isPhomeNumberValid(_value.ReceiverPhoneNumber)
                            || _value.OrderTickets.IsNullOrEmpty()
                            )
            {
                _message = "Invalid Request";
            }
            return;
        }
    }
}
