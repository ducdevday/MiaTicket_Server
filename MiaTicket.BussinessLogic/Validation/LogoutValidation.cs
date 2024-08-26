using MiaTicket.BussinessLogic.Request;

namespace MiaTicket.BussinessLogic.Validation
{
    public class LogoutValidation : BaseValidation<LogoutRequest>
    {
        public LogoutValidation(LogoutRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (base._value.userId == Guid.Empty) _message = "Invalid request";
            
        }
    }
}
