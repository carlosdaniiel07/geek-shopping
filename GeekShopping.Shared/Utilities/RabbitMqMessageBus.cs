using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
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

            _logger.LogInformation("Publishing message to {topic}: {message}", exchange, messageJson);

            channel.BasicPublish(exchange, string.Empty, null, messageBytes);

            await Task.CompletedTask;
        }
    }
}
