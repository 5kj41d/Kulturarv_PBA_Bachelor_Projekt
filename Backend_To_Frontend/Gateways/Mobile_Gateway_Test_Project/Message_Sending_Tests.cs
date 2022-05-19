using Xunit;
using Moq;
using System; 
using Mobile_Gateway.rabbitmq;

namespace Mobile_Gateway_Test_Project
{
public class Message_Sending_Tests
{
    #region Version 1 tests for search service controller. --> RabbitMQ Classes for now. 
    private const string Routing_key = "Search";
    private struct Valid_Sent_Model
    {
        public Valid_Sent_Model(string routing_key, string message)
        {
            _routing_key = routing_key; 
            _message = message; 
        }
        string _routing_key {get; set;}
        string _message {get; set;}
    }
    private struct Invalid_Sent_Model
    {
        public Invalid_Sent_Model(string routing_key, string message)
        {
            _routing_key = routing_key; 
            _message = message; 
        }
        string _routing_key {get; set;}
        string _message {get; set;}
        //Something odd should be used.
        
    }
    ///////////////////// Tests starts /////////////////////
    [Fact]
    public void Test_Rpc_Get_All()
    {
        //Arrange
        Valid_Sent_Model valid_Sent_Model = new Valid_Sent_Model(Routing_key,"Get_All");
        var mock_rabbitmq = new Mock<Rpc_sender_IF>(); 
        //Act 
        //Assert
    }
    [Fact]
    public void Test_Rpc_Get_By_Region()
    {
        //Arrange
        //Act
        //Assert
    }
    [Fact]
    public void Test_Rpc_Get_By_Timeage()
    {
        //Arrange
        //Act
        //Assert
        Console.WriteLine("Working!"); 
    }
    [Fact]
    public void Test_Rpc_Get_By_Heritage_Type()
    {
        //Arrange
        //Act
        //Assert
    }

    //TODO: Test for invalid inputs. 
    ///////////////////// Tests end /////////////////////
    #endregion
    }
}