using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchService.utils
{
    public static class JsonConverter
    {
        public static string ConvertDBResultToJson(List<IRecord> result)
        {
            foreach (var record in result)
            {
                foreach (KeyValuePair<string, object> entry in record.Values.As<INode>().Properties.Values)
                {
                    Console.WriteLine(entry.Value);
                }
                
            }
            return "something";
        }

    }
}
