using RabbitMQ.Client.Events;
using PaymentGateway.Infrastructure.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Common.Enums;

namespace PaymentGateway.Services
{
    public class RabbitMqConsumer : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;        
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private bool _disposed;

        public RabbitMqConsumer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
           // _serviceProvider = serviceProvider;
            var factory = new ConnectionFactory() { 
                HostName = "localhost",
                UserName = "guest",     // Default username
                Password = "guest"      // Default password
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: "payment_events", durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Failed to initialize RabbitMQ connection: {ex.Message}");
                throw;
            }
            
        }

        public void StartConsuming()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(RabbitMqConsumer));
            }
            try
            {
                // Create an instance of EventingBasicConsumer
                var consumer = new EventingBasicConsumer(_channel);

                // Define the event handler for when a message is received
                consumer.Received += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Received: {message}");

                        // Update database based on message
                        await UpdateTransactionStatusAsync(message);

                        // Acknowledge the message
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing message: {ex.Message}");

                        // Optionally, reject the message if processing fails
                        // _channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                    }
                };

                // Start consuming messages from the queue
                _channel.BasicConsume(queue: "payment_events",
                                      autoAck: false, // Set to false to manually acknowledge messages
                                      consumer: consumer);

                Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");
            }
            catch (Exception)
            {

                throw;
            }
            
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
                    transaction.Status = (int)MockPaymentStatusEnum.Success;
                    await _dbContext.SaveChangesAsync();
                }

                //using (var scope = _serviceProvider.CreateScope())
                //{
                //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //    var transaction = await dbContext.Transactions.FindAsync(transactionId);
                //    if (transaction != null)
                //    {
                //        transaction.Status = parts[1];
                //        await dbContext.SaveChangesAsync();
                //    }
                //}

            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _channel?.Close();
                    _connection?.Close();
                    _channel?.Dispose();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
