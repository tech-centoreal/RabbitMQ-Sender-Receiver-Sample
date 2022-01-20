using RabbitMQ.Client;
using System.Text;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Sender !");


var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "task-queue",
                         durable: true, // Messages survives RabbitMQ Restarts
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var message = ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    var body = Encoding.UTF8.GetBytes(message);

    var properties = channel.CreateBasicProperties();
    properties.Persistent = true;   // Makes messages persistent

    // Messages get distributed among the workers
    // One message is processed by only one worker
    channel.BasicPublish(exchange: "",
                         routingKey: "task-queue",
                         basicProperties: null,
                         body: body);
    Console.WriteLine(" [x] Sent {0}", message);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();