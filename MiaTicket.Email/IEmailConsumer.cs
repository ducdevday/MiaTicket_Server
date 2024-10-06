using MiaTicket.Email.Model;
using MiaTicket.Setting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace MiaTicket.Email
{
    public interface IEmailConsumer
    {
        void Consume();
    }

    public class EmailConsumer : IEmailConsumer
    {
        private EnviromentSetting _setting = EnviromentSetting.GetInstance();
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ROUTE_KEY = "mail_queue";

        public EmailConsumer(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: ROUTE_KEY, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailModel = JsonSerializer.Deserialize<EmailModel>(message);
                if (emailModel != null) SendMail(emailModel);
            };
            _channel.BasicConsume(ROUTE_KEY, autoAck: true, consumer: consumer);
        }


        private void SendMail(IEmailModel emailModel)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient(_setting.GetSMTPURL());

            message.From = new MailAddress(emailModel.Sender);
            message.To.Add(new MailAddress(emailModel.Receiver));
            message.Subject = emailModel.Subject;
            message.Body = emailModel.Body;
            message.IsBodyHtml = true;

            smtp.Port = _setting.GetSMTPPort();
            smtp.Credentials = new NetworkCredential(_setting.GetSMTPEmail(), _setting.GetSMTPAppPassword());
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
    }
}
