using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Twilio;

namespace QueueReader
{
    public class Program
    {
        public void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var client = new TwilioRestClient(Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"), Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("messages", false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("messages", true, consumer);

                    Console.WriteLine(" [*] Waiting for messages." +
                                             "To exit press CTRL+C");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var message = JsonConvert.DeserializeObject<Libs.Message>(Encoding.UTF8.GetString(ea.Body));
                        Console.WriteLine(" [x] Received {0}", message.Body);
                        
                        // Send a nice message back to each one of the Inbound Messages
                        client.SendMessage("", message.From, "Thanks for seeing me @ dmconf15. Here's some Twilio <3 to get you started, just use the code NOSQL15. Hit me up @marcos_placona");
                    }
                }
            }
        }
    }
}
