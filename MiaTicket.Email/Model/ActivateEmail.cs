namespace MiaTicket.Email.Model
{
    public class ActivateEmail : IEmailModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Body { set; get; }
        public string Subject { get; set; }
    }
}
