using MiaTicket.Email.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiaTicket.Email
{
    public interface IEmailProducer
    {
        void SendMessage(IEmailModel message);
    }
    public class EmailProducer : IEmailProducer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ROUTE_KEY = "mail_queue";
        public EmailProducer(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: ROUTE_KEY, true, false, false, null);
        }

        public void SendMessage(IEmailModel message)
        {
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            _channel.BasicPublish("", routingKey: ROUTE_KEY, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
        }
    }
}
