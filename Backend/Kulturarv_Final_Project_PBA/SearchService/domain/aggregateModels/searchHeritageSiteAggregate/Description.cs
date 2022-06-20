using System; 
using models; 

namespace aggregateModels
{
public class Description : ValueObject<Description>
{
    public string _Description {get; private set;}
    public Description(string Description)
    {
        _Description = Description; 
    }
    public bool Same_Value_As(Description desc)
    {
        return desc != null && this._Description.Equals(desc._Description); 
    }
}
}