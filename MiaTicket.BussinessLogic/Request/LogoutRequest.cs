namespace MiaTicket.BussinessLogic.Request
{
    public class LogoutRequest
    {
        public Guid userId { get; set; }
        public string refreshToken { get; set; } = string.Empty;

        public LogoutRequest(Guid userId, string? refreshToken)
        {
            this.userId = userId;
            this.refreshToken = refreshToken ?? "";
        }
    }
}
