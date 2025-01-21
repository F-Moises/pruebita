using Notification.Application.Contract;
using Notification.Application.Models;

namespace Notification.ConsumerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private IBrockerMessage _brockerMessage;

        private IMessageEmail _messageEmail;

        public Worker(ILogger<Worker> logger,IBrockerMessage brockerMessage, IMessageEmail messageEmail)
        {
            this._brockerMessage = brockerMessage;
            _messageEmail = messageEmail;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var emailToSend = await _brockerMessage.Consume(stoppingToken);
                    _messageEmail.SendEmailAsync(emailToSend);
                    Console.WriteLine("Se envio correctamente");
                }
                catch (Exception)
                {
                    throw;
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
