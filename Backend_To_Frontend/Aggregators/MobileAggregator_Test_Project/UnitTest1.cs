using System;
using Xunit;
using rabbitmq;
using System.Collections.Generic;

namespace MobileAggregator_Test_Project
{
    public class UnitTest1
    {
        struct MessageStruct
        {
            public string _message {get; private set;} 
            public MessageStruct(string message)
            {
                _message = message; 
            }
        }
        [Fact]
        public void Test_100_Message_RPC()
        {
            //Arrange
            int count_Messages_Returned = 0; 
            List<string> list_of_messages = new List<string>(); 
            //Act
            RabbitMqRPC rabbitmqRPC = new RabbitMqRPC(); 
            for(int i = 0; i <= 100; ++i)
            {
                MessageStruct message = new MessageStruct("Volapyk!");
                //rabbitmqRPC.
            }
            //Assert 
        }
    }
}
