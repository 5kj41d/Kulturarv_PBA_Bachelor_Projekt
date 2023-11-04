using System.Threading.Tasks;

public interface Search_Service_IF
{
    #region Version 1. 
    public void Search_By_Region(string regionName);
    public void Search_By_Heritage_Type(string heritageType);
    public void Search_By_Time_Age(string timeAgeName);
    public Task<string> Search_By_Top_10_Close_Heritage_Sites(double yourLongitude, double yourLatitude);
    public void Search_By_Institution_Event(string regionName);
    #endregion
}