using System;
using Microsoft.AspNet.Mvc;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace QueueWritter.Controllers
{
    public class HomeController : Controller
    {
        // http://twilio-queues.ngrok.io/Home/Index
        public IActionResult Index()
        {
            return Content("I'm running");
        }

        // http://twilio-queues.ngrok.io/Home/SayHello
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
