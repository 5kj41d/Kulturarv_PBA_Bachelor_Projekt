using System;  

namespace models
{
public class Location : ValueObject<Location>
{
    public string _Lattitude { get; private set;}
    public string _Longitude { get; private set; }
    public string _ID { get; private set; }
    public Location(string Lattitude, string Longitude)
    {
        _Lattitude = Lattitude; 
        _Longitude = Longitude; 
        _ID = Guid.NewGuid().ToString();
    }
    public bool Same_Value_As(Location value)
    {
        return value != null && this._ID.Equals(value._ID);
    }
}
}