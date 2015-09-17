using System;
using Microsoft.AspNet.Mvc;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace QueueWritter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("I'm running");
        }

        public void SayHello(Libs.Message incomingMessage)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("messages", false, false, false, null);
                    var message = JsonConvert.SerializeObject(incomingMessage);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "messages", null, body);
                }
            }
        }
    }
}
