namespace MiaTicket.Email.Model
{
    public interface IEmailModel
    {
        string Sender { get; set; }
        string Receiver { get; set; }
        string Body { get; }
        string Subject { get; set; }
    }
}
