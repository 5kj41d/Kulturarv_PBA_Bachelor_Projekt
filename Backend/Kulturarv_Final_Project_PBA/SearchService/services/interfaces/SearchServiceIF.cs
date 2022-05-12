using Microsoft.AspNetCore.Mvc;
public interface Search_Service_IF
{
    public void Search_By_Region();
    public void Search_By_Heritage_Type(); 
    public void Search_By_Time_Age(); 
    public void Search_By_Top_10_Close_Heritage_Sites();
    public void Search_By_Institution_Event();
}