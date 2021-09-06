using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApiLojas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LojaController : ControllerBase
    {
        public void Create()
        {


            var factory = new ConnectionFactory() { HostName = "192.168.0.161" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) ;
            {
                channel.QueueDeclare(
                        queue: "product_queue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                string message = System.Text.Json.JsonSerializer.Serialize(model);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                                    routingKey: "product_queue",
                                                    basicProperties: null,
                                                    body: body);



            }
        }
    }}
