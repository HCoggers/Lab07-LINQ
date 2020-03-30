using System;
using System.Collections.Generic;
using System.Text;

namespace Lab07_LINQ.Classes
{
    class Neighborhood
    {
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        
        public Neighborhood(float[] coords, string name = "unnamed")
        {
            Name = name;
            Lat = coords[0];
            Lon = coords[1];
        }
    }
}
