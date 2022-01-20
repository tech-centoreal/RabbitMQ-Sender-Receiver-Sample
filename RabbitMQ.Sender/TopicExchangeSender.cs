using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Sender
{
    public class TopicExchangeSender
    {
		public static void Produce(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

                // Identify the severity of the message so that it goes to respective receiver
                var topic = message.Split(":")[0];

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "topic_logs",
                                     routingKey: topic,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent '{0}':'{1}'", topic, message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
	}
}

