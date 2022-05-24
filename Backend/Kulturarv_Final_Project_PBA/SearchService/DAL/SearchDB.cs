using Neo4j.Driver;
using SearchService.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.DAL
{
    public class SearchDB : SearchDBIF
    {
        private readonly IDriver _driver;
        private readonly string uri;
        private readonly string user;
        private readonly string password;
        public SearchDB()
        {
            uri = "neo4j+s://2247b14c.databases.neo4j.io";
            user = "neo4j";
            password = "PnFCP9fOvFmj0nmVF3CoySRCmvKluE1Q565lMYjBGCI";
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public async Task<List<string>> Search_By_Heritage_Type(string heritageType)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> Search_By_Institution_Event(string regionName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> Search_By_Region(string regionName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> Search_By_Time_Age(string timeAgeName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> testFunction(double yourLongitude, double yourLatitude)
        {
            var result = await Search_By_Top_10_Close_Heritage_Sites(yourLongitude, yourLatitude);
            return null;
        }

        public async Task<List<IRecord>> Search_By_Top_10_Close_Heritage_Sites(double yourLongitude, double yourLatitude)
        {
            var query = @"With point({longitude: $yourLongitude, latitude:$yourLatitude}) as yourLocation
            MATCH (h:Heritage_Location)
            WHERE point.distance(yourLocation, h.Coordinate) < 50000000000
            RETURN h
            ORDER BY point.distance(yourLocation, h.Coordinate) ASC 
            LIMIT 10
            ";
            var session = _driver.AsyncSession();
            List<IRecord> readResults = null;
            List<string> resultJson = null;
            try
            {
                readResults = await session.ReadTransactionAsync(async tx =>
                    {
                        var result = await tx.RunAsync(query, new { yourLongitude = yourLongitude, yourLatitude = yourLatitude });
                        return (await result.ToListAsync());
                    });
            }
            //resultJson = JsonConverter.ConvertDBResultToJson(readResults);

            //Map to SearchServiceModel


            // Capture any errors along with the query and data for traceability
            catch (Neo4jException ex)
            {
                Console.WriteLine($"{query} - {ex}");
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
            return readResults;
        }
    }
}
