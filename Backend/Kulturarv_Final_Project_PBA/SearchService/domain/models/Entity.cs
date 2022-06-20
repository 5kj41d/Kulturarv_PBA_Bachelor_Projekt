using System; 

namespace models
{
public interface Entity<T>
{
    public bool Same_Identity_As(T obj);
}
}