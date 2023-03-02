using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Shared.Utilities
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly string _connectionString;
        private readonly ILogger<RabbitMqMessageBus> _logger;

        public RabbitMqMessageBus(IConfiguration configuration, ILogger<RabbitMqMessageBus> logger)
        {
            _connectionString = configuration.GetConnectionString("RabbitMq");
            _logger = logger;
        }

        public async Task PublishAsync<TEvent>(string exchange, TEvent data) where TEvent : BaseEvent
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_connectionString),
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var messageJson = JsonSerializer.Serialize(data);
            var messageBytes = Encoding.UTF8.GetBytes(messageJson);

            _logger.LogInformation("[RabbitMqMessageBus] publishing message to {topic}: {message}", exchange, messageJson);

            channel.BasicPublish(exchange, string.Empty, null, messageBytes);

            await Task.CompletedTask;
        }

        public void Subscribe<TEvent>(string queue, Func<TEvent, Task<bool>> handler) where TEvent : BaseEvent
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_connectionString),
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.ASCII.GetString(body);
                    var success = await handler(JsonSerializer.Deserialize<TEvent>(messageJson));

                    if (success)
                        channel.BasicAck(ea.DeliveryTag, false);
                    else
                        channel.BasicNack(ea.DeliveryTag, false, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[RabbitMqMessageBus] error while processing event from {queue} queue", queue);
                    channel.BasicNack(ea.DeliveryTag, false, false);
                }
            };

            channel.BasicConsume(queue, false, consumer);
        }
    }
}
