using System; 

namespace models
{
public interface ValueObject<T>
{
    public bool Same_Value_As(T value);
}
}