using MiaTicket.BussinessLogic.Request;

namespace MiaTicket.BussinessLogic.Validation
{
    public class GenerateRefreshTokenValidation : BaseValidation<GenerateTokenRequest>
    {
        public GenerateRefreshTokenValidation(GenerateTokenRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.RefreshToken)) _message = "Invalid request";
        }
    }
}
