using System; 
using models; 

namespace aggregateModels
{
    //Aggregate Root.
public class SearchHeritageSiteAggregator : Entity<SearchHeritageSiteAggregator>
{
     private HeritageSite _HeritageSite; 
     private Description _Description; 
     public string _ID {get; private set; }

    public SearchHeritageSiteAggregator(HeritageSite HeritageSite, Description Description)
    {
        if(HeritageSite != null)
            _HeritageSite = HeritageSite; 
            else {Console.WriteLine("Heritage site must be specified!");}
        if(Description != null)
            _Description = Description; 
            else {Console.WriteLine("Description must be specified!");}

        _ID = Guid.NewGuid().ToString(); 
    }

    public bool Equals(Object obj)
    {
        if(this == obj)
            return true; 
        if(obj == null || this != obj) 
            return false; 
        
        SearchHeritageSiteAggregator searchHeritageSiteAggregator = (SearchHeritageSiteAggregator) obj; 
        return Same_Identity_As(searchHeritageSiteAggregator);
    }

    public bool Same_Identity_As(SearchHeritageSiteAggregator siteAggregator)
    {
        return siteAggregator != null && this._ID.Equals(siteAggregator._ID);
    }   
}
}