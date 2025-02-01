using RabbitMQ.Client.Events;
using PaymentGateway.Infrastructure.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class RabbitMqConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly AppDbContext _dbContext;


        public RabbitMqConsumer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "payment_events", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");

                // Update database based on message
                await UpdateTransactionStatusAsync(message);
            };
            _channel.BasicConsume(queue: "payment_events", autoAck: true, consumer: consumer);
        }

        private async Task UpdateTransactionStatusAsync(string message)
        {
            // Parse message and update transaction status
            var parts = message.Split(" - ");
            if (parts.Length != 2) return;

            if (int.TryParse(parts[0].Split(' ')[1], out int transactionId))
            {
                var transaction = await _dbContext.Transactions.FindAsync(transactionId);
                if (transaction != null)
                {
                    transaction.Status = parts[1];
                    await _dbContext.SaveChangesAsync();
                }
            }
        }


    }
}
