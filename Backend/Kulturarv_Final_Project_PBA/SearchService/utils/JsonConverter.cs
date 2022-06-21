using Neo4j.Driver;
using System.Text.Json;
using SearchService.models.queriy;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchService.utils
{
    public static class JsonConverter
    {
        public static string ConvertDBResultToJson(List<IRecord> result)
        {
            List<HeritageLocationSearchQueryModel> toBeSerialized = new List<HeritageLocationSearchQueryModel>();
            
            foreach (var record in result)
            {
                HeritageLocationSearchQueryModel model = new HeritageLocationSearchQueryModel();
                model.Name = record["h.Name"].As<String>();
                model.Origin_Year = record["h.Origin_Year"].As<String>();
                model.Terrain_Type = record["h.Terrain_Type"].As<String>();
                model.Type = record["h.Type"].As<String>();
                model.Basic_Information = record["h.Basic_Information"].As<String>();
                model.Coordinate = record["h.Coordinate"].As<String>();
                toBeSerialized.Add(model);

                //h.Name, h.Coordinate, h.Terrain_Type, h.Basic_Information, h.Origin_Year, h.Type
                //record.Values.As<INode>().Properties
                //foreach (KeyValuePair<string, object> entry in record.Values.As<INode>().Properties.Values)
                //{
                //    Console.WriteLine(entry.Value);
                //}

            }
            var json = JsonSerializer.Serialize(toBeSerialized);
            return json;
        }

    }
}
