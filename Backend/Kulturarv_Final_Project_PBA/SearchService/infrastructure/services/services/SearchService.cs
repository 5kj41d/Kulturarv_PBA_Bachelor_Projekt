using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchService.DAL;

//https://stackoverflow.com/questions/71527891/versioning-in-c-sharp-asp-net-core
[ApiVersion("1.0")]
//[ApiVersion("2.0")]
public class Search_Service : Search_Service_IF
{
    //TODO: Search for a ....
    //TODO: 
    #region Version 1.
    private readonly SearchDBIF searchDB;

    public Search_Service()
    {
        searchDB = new SearchDB();
    }

    //[MapToApiVersion("2.0")]
    //--> Take picture and sent. --> This should be validated?
    //Safe information --> This should be validated?
    #endregion
    public void Search_By_Heritage_Type(string heritageType)
    {
        throw new NotImplementedException();
    }

    public void Search_By_Institution_Event(string regionName)
    {
        throw new NotImplementedException();
    }

    public void Search_By_Region(string regionName)
    {
        throw new NotImplementedException();
    }

    public void Search_By_Time_Age(string timeAgeName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Search_By_Top_10_Close_Heritage_Sites(double yourLongitude, double yourLatitude)
    {

        var result = await searchDB.Search_By_Top_10_Close_Heritage_Sites(yourLongitude, yourLatitude);
        return result;
    }
}