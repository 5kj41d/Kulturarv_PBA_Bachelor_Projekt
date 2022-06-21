using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using Mobile_Gateway; 
using mobile_gateway_models;

namespace Mobile_Gateway.rabbitmq
{
    //Ressource: https://www.rabbitmq.com/tutorials/tutorial-six-dotnet.html
    public interface Rpc_sender_IF
    {
        public string Call(string message);
        public void Close();
        public bool Test_Connection();
    }
    public class Rpc_sender : Rpc_sender_IF
    {
         private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;
        private readonly ILogger _logger;
        private readonly RabbitMqConfiguration _configuration;

        public Rpc_sender(IOptions<RabbitMqConfiguration> options, ILogger logger)
        {
            _configuration = options.Value;
            _logger = logger;
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = _configuration._user;
            factory.Password = _configuration._pass;
            factory.VirtualHost = _configuration._vhost;
            factory.HostName = _configuration._hostName;
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                respQueue.Add(response);
            }
        };

        channel.BasicConsume(
            consumer: consumer,
            queue: replyQueueName,
            autoAck: true);
    }

    public string Call(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(
            exchange: "amq.direct",
            routingKey: "RPC_Request_Search_Queue",
            basicProperties: props,
            body: messageBytes);

        return respQueue.Take();
    }

        public void Close()
        {
            connection.Close();
        }

        public string Sent_Message_To_Message_Bus_RPC()
        {

            return ""; 
        }


        /// <summary>
        /// Setup connection to RabbitMQ.
        /// </summary>
        /// <returns>IConnection object</returns>
        private IConnection Setup_Connection()
        {
            IConnection conn = null;
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = _configuration._user;
                factory.Password = _configuration._pass;
                factory.VirtualHost = _configuration._vhost;
                factory.HostName = _configuration._hostName;
                conn = factory.CreateConnection();
            }
            catch (Exception e)
            {
                _logger.LogError(0, e, "Couldnt connect to RabbitMQ server", conn);
            }
            return conn;
        }

        /// <summary>
        /// Only for test purpose. Code duplication. 
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Test_Connection()
        {
            IConnection conn = null;
            IModel channel = null;
            try
            {
                conn = Setup_Connection();
                channel = conn.CreateModel();
                byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("TEST!");
                IBasicProperties props = channel.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.DeliveryMode = 2;
                channel.BasicPublish("direct", "Search", props, messageBodyBytes);
            }
            catch (Exception)
            {
                Console.WriteLine("Couldnt sent message to RabbitMQ!");
                return false;
            }
            finally
            {
                conn.Close();
                channel.Close();
            }
            return true;
        }
    }
}