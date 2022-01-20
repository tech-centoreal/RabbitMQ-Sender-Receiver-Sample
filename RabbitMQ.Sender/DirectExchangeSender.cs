using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Sender
{
    // A message goes to the queues whose binding key exactly matches the routing key of the message
    public class DirectExchangeSender
    {
		public static void Produce(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                // Identify the severity of the message so that it goes to respective receiver
                var severity = message.Split(":")[0];

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "direct_logs",
                                     routingKey: severity,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
	}
}

