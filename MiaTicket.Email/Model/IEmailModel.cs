namespace MiaTicket.Email.Model
{
    public interface IEmailModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Body { get; }
        public string Subject { get; set; }
    }
}
