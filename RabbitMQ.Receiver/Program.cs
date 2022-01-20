using System;
using RabbitMQ.Receiver;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Receiver !");

// FanoutExchangeReceiver.Consume();

// DirectExchangeReceiver.Consume(args);

TopicExchangeReceiver.Consume(args);
