
using RabbitMQ.Client;
using System.Text;

namespace PaymentGateway.Services
{
    public class RabbitMqPublisher : IRabbitMqPublisher, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMqPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "payment_events", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        public async Task PublishEvent(string eventType, string transactionId)
        {
            var message = $"{{\"EventType\":\"{eventType}\",\"TransactionId\":\"{transactionId}\"}}";
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "payment_events", basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
