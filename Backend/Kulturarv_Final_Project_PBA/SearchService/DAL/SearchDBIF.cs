using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.DAL
{
    public interface SearchDBIF
    {
        #region Version 1. 
        public Task<List<string>> Search_By_Region(string regionName);
        public Task<List<string>> Search_By_Heritage_Type(string heritageType);
        public Task<List<string>> Search_By_Time_Age(string timeAgeName);
        public Task<List<string>> testFunction(double yourLongitude, double yourLatitude);
        public Task<List<IRecord>> Search_By_Top_10_Close_Heritage_Sites(double yourLongitude, double yourLatitude);
        public Task<List<string>> Search_By_Institution_Event(string regionName);
        #endregion
    }
}
