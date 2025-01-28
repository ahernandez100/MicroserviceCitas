using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RecetasServices.Application.Commans;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecetasServices.Application.Services
{
    public class RabbitMQListener
    {
        private readonly IMediator _mediator;
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;
        private readonly string _queueName;
        public RabbitMQListener(IMediator mediator)
        {
            _mediator = mediator;
            _hostName = ConfigurationManager.AppSettings["RabbitMQHost"];
            _port = int.Parse(ConfigurationManager.AppSettings["RabbitMQPort"]);
            _userName = ConfigurationManager.AppSettings["RabbitMQUsername"];
            _password = ConfigurationManager.AppSettings["RabbitMQPassword"];
            _virtualHost = ConfigurationManager.AppSettings["RabbitMQVirtualHost"];
            _queueName = ConfigurationManager.AppSettings["RabbitMQQueueName"];
        }
        public void StartListening()
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var receta = JsonConvert.DeserializeObject<AddRecetaRequest>(message);

                    // Procesar la receta recibida
                    await HandleReceta(receta);
                };

                channel.BasicConsume(queue: _queueName,
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" [*] Esperando mensajes. Presione [enter] para salir.");
                Console.ReadLine();
            }
        }
        private async Task HandleReceta(AddRecetaRequest receta)
        {
            Console.WriteLine($" [x] Recibido receta para Cita ID: {receta.Commnad.codigoCita}, Paciente con codigo: {receta.Commnad.codigoPaciente}");
             int result =(int) _mediator.Send(receta).Result;
            Console.WriteLine($" [x] Receta creada con el codigo: {result}");
        }
    }
}