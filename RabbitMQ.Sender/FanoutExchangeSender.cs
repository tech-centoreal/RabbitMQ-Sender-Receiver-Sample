using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Sender
{
	public class FanoutExchangeSender
    {
		public static void Produce(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",  // its value is ignored for fanout exchanges
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
	}
}

