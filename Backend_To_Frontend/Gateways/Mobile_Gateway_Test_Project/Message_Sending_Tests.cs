using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mobile_Gateway.rabbitmq;
using Moq;
using Xunit;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mobile_Gateway_Test_Project
{


    public class Message_Sending_Tests
    {
        #region Version 1 tests for search service controller. --> RabbitMQ Classes for now.
        private const string Routing_key = "Search";
        public IServiceProvider Services { get; private set; }
        //public Logger Logger { get; private set; }
        public IOptions<RabbitMqConfiguration> _options; 
        public IConfiguration Configuration { get; }
        public Message_Sending_Tests()
        {
           Configure(); 
        }

        private void Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json")
                //.AddJsonFile("appsettings.test.json", optional: true)
                .Build();

            // Logger = new LoggerConfiguration()
            // .MinimumLevel.Debug()
            // .WriteTo.LiterateConsole()
            // .WriteTo.RollingFile("logs/{Date}-log.txt")
            // .CreateLogger();

            var services = new ServiceCollection();
            // services.AddSingleton<ILogger>(s => Logger);
            // other DI logic and initializations ...
            //services.AddTransient(x => ...);

            services.Configure<RabbitMqConfiguration>(a => Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
            Services = services.BuildServiceProvider();
        }

        ///////////////////// Tests starts /////////////////////
        [Fact]
        public void Test_Rpc_Get_All()
        {
            //Arrange
            Sent_Model sent_Model = new Sent_Model(Routing_key, "Get_All");
            var mock_rabbitmq = new Mock<Rpc_sender_IF>();

            //Act
            mock_rabbitmq
                .Setup(p => p.Sent_Message_To_Message_Bus_RPC(sent_Model))
                .Returns("Something");
            var result =
                mock_rabbitmq
                    .Object
                    .Sent_Message_To_Message_Bus_RPC(sent_Model);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Rpc_Get_By_Region()
        {
            //Arrange
            Sent_Model sent_Model = new Sent_Model(Routing_key, "Region");
            var mock_rabbitmq = new Mock<Rpc_sender_IF>();

            //Act
            mock_rabbitmq
                .Setup(p => p.Sent_Message_To_Message_Bus_RPC(sent_Model))
                .Returns("Something");
            var result =
                mock_rabbitmq
                    .Object
                    .Sent_Message_To_Message_Bus_RPC(sent_Model);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Rpc_Get_By_Timeage()
        {
            //Arrange
            Sent_Model sent_Model = new Sent_Model(Routing_key, "TimeAge");
            var mock_rabbitmq = new Mock<Rpc_sender_IF>();

            //Act
            mock_rabbitmq
                .Setup(p => p.Sent_Message_To_Message_Bus_RPC(sent_Model))
                .Returns("Something");
            var result =
                mock_rabbitmq
                    .Object
                    .Sent_Message_To_Message_Bus_RPC(sent_Model);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Rpc_Get_By_Heritage_Type_Burial_Mound()
        {
            //Arrange
            Sent_Model sent_Model = new Sent_Model(Routing_key, "Gravhøj");
            var mock_rabbitmq = new Mock<Rpc_sender_IF>();

            //Act
            mock_rabbitmq
                .Setup(p => p.Sent_Message_To_Message_Bus_RPC(sent_Model))
                .Returns("Something");
            var result =
                mock_rabbitmq
                    .Object
                    .Sent_Message_To_Message_Bus_RPC(sent_Model);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Rpc_Get_With_Invalid_Input_Integer()
        {
            //Arrange
            Sent_Model sent_Model = new Sent_Model(Routing_key, "12345");
            var mock_rabbitmq = new Mock<Rpc_sender_IF>();

            //Act
            mock_rabbitmq
                .Setup(p => p.Sent_Message_To_Message_Bus_RPC(sent_Model))
                .Returns("Not valid!");
            var result =
                mock_rabbitmq
                    .Object
                    .Sent_Message_To_Message_Bus_RPC(sent_Model);

            //Assert
            Assert.Equal("Not valid!", result);
        }

        [Fact]
        public void Test_Rpc_Get_With_Invalid_Input_String()
        {
            //Arrange
            Sent_Model sent_Model = new Sent_Model(Routing_key, "Volapyk");
            var mock_rabbitmq = new Mock<Rpc_sender_IF>();

            //Act
            mock_rabbitmq
                .Setup(p => p.Sent_Message_To_Message_Bus_RPC(sent_Model))
                .Returns("Not valid!");
            var result =
                mock_rabbitmq
                    .Object
                    .Sent_Message_To_Message_Bus_RPC(sent_Model);

            //Assert
            Assert.Equal("Not valid!", result);
        }

        [Fact]
        public void Test_Rpc_Connection_To_RabbitMQ()
        {
            //TODO: get configs from TestFixture. 
            var config = Services.GetService<RabbitMqConfiguration>();
            Rpc_sender rpc_Sender = new Rpc_sender(_options, null);
            var result = rpc_Sender.Test_Connection();
            Assert.True(result);
        }
        ///////////////////// Tests end /////////////////////
        #endregion
    }
}
