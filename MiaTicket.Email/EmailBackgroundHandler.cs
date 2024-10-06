using Microsoft.Extensions.Hosting;

namespace MiaTicket.Email
{
    public class EmailBackgroundHandler : IHostedService
    {
        private readonly IEmailConsumer _emailConsumer;

        public EmailBackgroundHandler(IEmailConsumer emailConsumer)
        {
            _emailConsumer = emailConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                _emailConsumer.Consume();
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
