using System; 

public interface ValueObject<t>
{
    bool Same_Value_As(t obj);
}