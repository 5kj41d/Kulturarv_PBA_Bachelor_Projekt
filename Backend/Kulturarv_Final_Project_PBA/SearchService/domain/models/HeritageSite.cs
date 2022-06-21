using System; 

namespace models
{
public class HeritageSite : Entity<HeritageSite>
{
    public string _Name {get;private set;}
    public string _ID {get; private set;}
    public HeritageSite(string name, string ID)
    {
        _Name = name; 
        _ID = ID; 
    }

    public bool Same_Identity_As(HeritageSite site)
    {
        return site != null && this._ID.Equals(site._ID);
    }
}
}