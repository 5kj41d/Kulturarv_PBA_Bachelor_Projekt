using System;
using System.Collections.Generic;
using System.Text;

namespace SearchService.models.queriy
{
    public class HeritageLocationSearchQueryModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Terrain_Type { get; set; }
        public string Coordinate { get; set; }
        public string Origin_Year { get; set; }
        public string Basic_Information { get; set; }
    }
}
