using System; 
using models; 

namespace aggregateModels
{
public class Description : ValueObject<Description>
{
    public string _Description {get; private set;}
    public Description()
    {

    }
    public bool Same_Value_As(Description desc)
    {
        return false; 
    }
}
}