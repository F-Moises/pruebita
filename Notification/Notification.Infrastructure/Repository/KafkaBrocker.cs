using Confluent.Kafka;
using Notification.Application.Contract;
using Notification.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Repository
{
    public class KafkaBrocker : IBrockerMessage
    {
        public async Task<EmailToSend> Consume(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            try
            {
                consumer.Subscribe("user-created");
                var mensajeRescatado = consumer.Consume(cancellationToken);
                EmailToSend emailToSend = JsonSerializer.Deserialize<EmailToSend>(mensajeRescatado.Message.Value);
                return emailToSend;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
