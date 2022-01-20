using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Receiver
{
	public class FanoutExchangeReceiver
	{
		public static void Consume()
		{
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                // Let the server choose a random queue name for us
                // Create a non-durable, exclusive, autodelete queue with a generated name
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: "logs",
                                  routingKey: "" // Ignored for Fanout exchange type
                                  );

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
	}
}

