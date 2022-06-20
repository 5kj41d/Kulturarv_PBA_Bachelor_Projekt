using System; 

namespace models
{
public class HeritageSite : Entity<HeritageSite>
{
    public HeritageSite()
    {

    }

    public bool Same_Identity_As(HeritageSite site)
    {
        return false; 
    }
}
}