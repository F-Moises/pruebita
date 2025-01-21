using Confluent.Kafka;
using Identity.Application.Contracts.Repositories;
using Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repository
{
    public class KafkaBrocker : IBrockerMessage
    {
        public async Task Produce(EmailToSend message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var result = await producer.ProduceAsync("user-created", new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(message)
            });
        }
    }
}
