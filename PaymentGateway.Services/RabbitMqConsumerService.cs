using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PaymentGateway.Common.Enums;
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
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider; // Use IServiceProvider to create scopes
        private readonly IModel _channel;

        public RabbitMqConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

             var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: "payment_events", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await HandleEvent(message);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: "payment_events", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        private async Task HandleEvent(string message)
        {
            try
            {
                var eventData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

                if (eventData["EventType"] == "payment_initiated")
                {
                    var transactionId = Guid.Parse(eventData["TransactionId"]);
                    Console.WriteLine($"Payment initiated: {transactionId}");

                    
                    await Task.Delay(5000); // Simulate 5-second delay

                    // Create a new scope to resolve AppDbContext
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        // Update transaction status in DB
                        var transaction =  await dbContext.Transactions.FirstOrDefaultAsync(x=> x.TransactionID == transactionId);
                        if (transaction != null)
                        {
                            transaction.Status = (int)MockPaymentStatusEnum.Completed; // Simulated final status
                            await dbContext.SaveChangesAsync();
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling event: {ex.Message}");
            }
        }
    }
}
