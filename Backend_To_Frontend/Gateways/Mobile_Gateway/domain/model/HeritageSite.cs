using System; 

public class HeritageSite : Entity<HeritageSite>
{
    //Attributes - Private set. Public get. 
    private string _trackingId {private set; get;}

    public HeritageSite()
    {
        
    }

    /// <summary>
    /// Checking the identity of the object by Id. 
    /// </summary>
    /// <param name="site"></param>
    /// <returns></returns>
    public bool Same_Identity_As(HeritageSite site)
    {
        //return site != null && this._trackingId.Same_Value_As(THE VALUE OBJECT);
    }
}