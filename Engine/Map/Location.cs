using System;

namespace Engine.Map
{
    [Serializable]
    public class Location : ICloneable
    {
        private readonly int _latitude;
        public int Latitude
        {
            get { return _latitude; }
        }

        private readonly int _longitude;
        public int Longitude
        {
            get { return _longitude; }
        }

        public Location(LocationFactory locationFactory)
        {
            int[] coordinates = locationFactory.MakeCoordinates();
            _latitude = coordinates[0];
            _longitude = coordinates[1];
        }

        public Location(int latitude, int longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Location))
                return false;

            Location loc = (Location) obj;

            return this.Latitude == loc.Latitude &&
                   this.Longitude == loc.Longitude;
        }

        public object Clone()
        {
            return new Location(_latitude, _longitude);
        }

        public double AbsoluteDistanceFrom(Location other)
        {
            int xDistance = this.Latitude - other.Latitude;
            if (xDistance < 0) xDistance = -xDistance;

            int yDistance = this.Longitude - other.Longitude;
            if (yDistance < 0) yDistance = -yDistance;

            return Math.Pow(
                Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2)
                , 0.5
            );
        }
    }
}