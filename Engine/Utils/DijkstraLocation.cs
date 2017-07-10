using Engine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utils
{
    public class DijkstraLocation : Location
    {
        public DijkstraLocation Predecessor { get; set; }

        public int Weight { get; set; }

        public DijkstraLocation(int latitude, int longitude, DijkstraLocation predecessor) : base(latitude, longitude)
        {
            Predecessor = predecessor;
            Weight = int.MaxValue;
        }

        public Location ToLocation()
        {
            return new Location(this.Latitude, this.Longitude);
        }
    }
}
