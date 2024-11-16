using MiaTicket.BussinessLogic.Request;

namespace MiaTicket.BussinessLogic.Validation
{
    public class CheckInEventValidation : BaseValidation<CheckInEventRequest>
    {
        public CheckInEventValidation(CheckInEventRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Code) || _value.Code.Length != 8) {
                _message = "InValid Input";
            }
        }
    }
}
