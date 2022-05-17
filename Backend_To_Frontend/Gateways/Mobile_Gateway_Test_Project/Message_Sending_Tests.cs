using Xunit;

namespace Mobile_Gateway_Test_Project;

public class Message_Sending_Tests
{
    #region Version 1 tests
    struct Valid_Sent_Model
    {
        string routing_key;
        string message;
    }
    struct Invalid_Sent_Model
    {
        string routing_key;
        string message; 
        //Something odd should be used.
    }
    [Fact]
    public void Test_Rpc_Get_All()
    {

    }
    [Fact]
    public void Test_Rpc_Get_By_Region()
    {

    }
    [Fact]
    public void Test_Rpc_Get_By_Timeage()
    {
        
    }
    [Fact]
    public void Test_Rpc_Get_By_Heritage_Type()
    {
        
    }
    //Test for invalid inputs. 
    #endregion
}