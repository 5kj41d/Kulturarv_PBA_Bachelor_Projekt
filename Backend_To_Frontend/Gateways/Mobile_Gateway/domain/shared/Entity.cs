using System; 

public interface Entity<t>
{
    bool Same_Identity_As(t obj);
}