using Microsoft.AspNetCore.Mvc;
public interface Search_Service_IF
{
    [ApiVersion("1.0")]
    public void Search_By_Region();
    [ApiVersion("1.0")]
    public void Search_By_Heritage_Type(); 
    [ApiVersion("1.0")]
    public void Search_By_Time_Age(); 
    [ApiVersion("1.0")]
    public void Search_By_Top_10_Close_Heritage_Sites();
    [ApiVersion("1.0")] 
    public void Search_By_Institution_Event();
}