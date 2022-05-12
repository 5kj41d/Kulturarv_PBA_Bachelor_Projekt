using System;
using Microsoft.AspNetCore.Mvc;

//https://stackoverflow.com/questions/71527891/versioning-in-c-sharp-asp-net-core
[ApiVersion("1.0")] 
//[ApiVersion("2.0")]
public class Search_Service : Search_Service_IF
{
    //TODO: Search for a ....
    //TODO: 
    public Search_Service()
    {
        Console.WriteLine("Search Service object init"); 
    }
    public void Search_By_Heritage_Type()
    {
        throw new NotImplementedException();
    }
    public void Search_By_Institution_Event()
    {
        throw new NotImplementedException();
    }

    public void Search_By_Region()
    {
        throw new NotImplementedException();
    }

    public void Search_By_Time_Age()
    {
        throw new NotImplementedException();
    }

    public void Search_By_Top_10_Close_Heritage_Sites()
    {
        throw new NotImplementedException();
    }

    //[MapToApiVersion("2.0")]
    //--> Take picture and sent. --> This should be validated?
    //Safe information --> This should be validated?
}