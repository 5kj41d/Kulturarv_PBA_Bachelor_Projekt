using Mobile_Gateway.rabbitmq;
using Moq;
using Xunit;
using mobile_gateway_models;

namespace Mobile_Gateway_Test_Project
{
    public class Message_Sending_Tests
    {
        #region Version 1 tests for search service controller. --> RabbitMQ Classes for now.
        private const string Routing_key = "Search";
        
        ///////////////////// Tests starts /////////////////////
        [Fact]
        public void Test_Rpc_Get_All()
        {
            //Arrange
            SentModel sent_Model = new SentModel(Routing_key, "Get_All");
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
            SentModel sent_Model = new SentModel(Routing_key, "Region");
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
            SentModel sent_Model = new SentModel(Routing_key, "TimeAge");
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
            SentModel sent_Model = new SentModel(Routing_key, "Gravhøj");
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
            SentModel sent_Model = new SentModel(Routing_key, "12345");
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
            SentModel sent_Model = new SentModel(Routing_key, "Volapyk");
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
            //TODO: Check if appsettings.test.json can be used. 
        }
        ///////////////////// Tests end /////////////////////
        #endregion
    }
}
