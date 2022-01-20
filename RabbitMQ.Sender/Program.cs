using RabbitMQ.Sender;
using System.Text;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Sender !");

var message = ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");

// DefaultExchangeSender.Produce(message);

// FanoutExchangeSender.Produce(message);

// DirectExchangeSender.Produce(message);

TopicExchangeSender.Produce(message);