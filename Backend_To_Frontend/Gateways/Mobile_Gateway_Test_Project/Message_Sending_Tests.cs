using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mobile_Gateway;
using Mobile_Gateway.rabbitmq;
using Moq;
using Xunit;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Mobile_Gateway_Test_Project
{
    public class TestsFixture : IDisposable
    {
        public readonly ILogger<Search_Service_Controller> _logger;
        public readonly IOptions<RabbitMqConfiguration> _configuration;
        public IConfiguration Configuration { get; }
        public TestsFixture(ILogger<Search_Service_Controller> logger, IOptions<RabbitMqConfiguration> options, IConfiguration configuration)
        {
            // Do "global" initialization here; Only called once.
            _logger = logger; 
            _configuration = options;
            Configuration = configuration; 
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //Add Logging DI service:
            var serviceProvider = services.BuildServiceProvider(); 
            var logger = serviceProvider.GetService<ILogger<Search_Service_Controller>>();
            services.AddSingleton(typeof(ILogger), logger);
            //RabbitMQ configuration.
            services.Configure<RabbitMqConfiguration>(a => Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
            services.AddSingleton<Rpc_sender_IF, Rpc_sender>();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }

    public class Message_Sending_Tests : IClassFixture<TestsFixture>
    {
        #region Version 1 tests for search service controller. --> RabbitMQ Classes for now.
       private const string Routing_key = "Search";
       private readonly TestsFixture _testsFixture; 
       private Message_Sending_Tests(TestsFixture testsFixture)
       {
           _testsFixture = testsFixture; //Get data from testsfixture. 
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
            Assert.NotNull (result);
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
            Assert.NotNull (result);
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
            Assert.NotNull (result);
        }

        [Fact]
        public void Test_Rc_Get_By_Heritage_Type_Burial_Mound()
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
            Assert.NotNull (result);
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
        }
        ///////////////////// Tests end /////////////////////
    #endregion
    }
}
