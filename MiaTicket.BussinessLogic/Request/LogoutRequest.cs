namespace MiaTicket.BussinessLogic.Request
{
    public class LogoutRequest
    {
        public Guid userId { get; set; }

        public LogoutRequest(Guid userId)
        {
            this.userId = userId;
        }
    }
}
