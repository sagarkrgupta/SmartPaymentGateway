
using RabbitMQ.Client;
using System.Text;

namespace PaymentGateway.Services
{
    public class RabbitMqPublisher
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
        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "payment_events", basicProperties: null, body: body);
        }
    }
}
