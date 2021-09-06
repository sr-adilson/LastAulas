using ConsoleLoja.Model;
using ConsoleLoja.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace ConsoleLoja
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseRepo<Loja> repo = new BaseRepo<Loja>(); 
            var factory = new ConnectionFactory() { HostName = "192.168.0.161" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var produto = System.Text.Json.JsonSerializer.Deserialize<Loja>(body);
                    repo.Create(produto);
                    Console.WriteLine(" [x] Received {0}", produto.Nome);
                };
                channel.BasicConsume(queue: "loja_queue",
                                                            autoAck: true,
                                                            consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }
    }
}
