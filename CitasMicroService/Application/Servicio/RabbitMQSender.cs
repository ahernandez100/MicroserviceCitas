using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Configuration;
using System.Text;

namespace CitasMicroService.Application.Servicio
{
    public class RabbitMQSender: IRabbitMQSender
    {
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;
        private readonly string _queueName;
        public RabbitMQSender()
        {
            _hostName = ConfigurationManager.AppSettings["RabbitMQHost"];
            _port = int.Parse(ConfigurationManager.AppSettings["RabbitMQPort"]);
            _userName = ConfigurationManager.AppSettings["RabbitMQUsername"];
            _password = ConfigurationManager.AppSettings["RabbitMQPassword"];
            _virtualHost = ConfigurationManager.AppSettings["RabbitMQVirtualHost"];
            _queueName = ConfigurationManager.AppSettings["RabbitMQQueueName"];
        }

        public void SendMessage(object message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = _port,
                UserName = _userName,
                Password = _password,
                VirtualHost = _virtualHost
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}