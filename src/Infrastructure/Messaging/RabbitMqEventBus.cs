using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using RabbitMQ.Client;

namespace Infrastructure.Messaging;

public class RabbitMqEventBus : IMessageBus
{
    public async Task PublishAsync(string topicOrQueueName, object message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "catalog", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var messageString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageString);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "catalog", body: body);

    }

    public Task SubscribeAsync<T>(string topicOrQueueName, Func<T, Task> onMessageReceived)
    {
        throw new NotImplementedException();
    }
}