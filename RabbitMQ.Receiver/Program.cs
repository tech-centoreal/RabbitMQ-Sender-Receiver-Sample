using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Receiver !");


var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "task-queue",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);
    // Tells RabbitMQ not to give more than one message to a worker at a time
    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);

        int dots = message.Split('.').Length - 1;
        Thread.Sleep(dots * 5000);

        Console.WriteLine(" [x] Done");

        // Manual acknowledgement. Default timeout on RabbitMQ server is 30 mins
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    };
    channel.BasicConsume(queue: "task-queue",
                         autoAck: false,    // Automatic acknowledgement On?
                         consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}