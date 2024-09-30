using MiaTicket.BussinessLogic.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class UpdatePaymentZaloPayValidation : BaseValidation<UpdatePaymentZaloPayRequest>
    {
        public UpdatePaymentZaloPayValidation(UpdatePaymentZaloPayRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.TransactionCode)) {
                _message = "Invalid Request";
            }
        }
    }
}
