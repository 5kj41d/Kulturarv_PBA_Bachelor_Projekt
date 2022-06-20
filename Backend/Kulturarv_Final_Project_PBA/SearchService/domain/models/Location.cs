using System; 

namespace models
{
public class Location : ValueObject<Location>
{
    public string _Lattitude { get; private set;}
    public string _Longitude { get; private set; }
    public Location(string Lattitude, string Longitude)
    {
        _Lattitude = Lattitude; 
        _Longitude = Longitude; 
    }
    public bool Same_Value_As(Location value)
    {
        return false; 
    }
}
}