namespace MiaTicket.Email.Model
{
    public interface IEmailModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
