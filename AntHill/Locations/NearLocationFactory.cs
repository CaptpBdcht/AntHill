using Anthill.Zones;
using Engine;
using Engine.Map;

namespace Anthill.Locations
{
    public class NearLocationFactory : LocationFactory
    { 
        private readonly Ant _ant;

        private readonly World _world;

        public NearLocationFactory(Ant ant, World world)
        {
            _ant = ant;
            _world = world;
        }

        public override int[] MakeCoordinates()
        {
            bool latPlus = false,
                 latMinus = false,
                 lonPlus = false,
                 lonMinus = false;

            int latitude = _ant.Location.Latitude;
            int longitude = _ant.Location.Longitude;

            int move = BoardMetadata.Random.Next(0, 8);
            switch (move)
            {
                case 0: latMinus = true; break;
                case 1: latMinus = true; lonPlus = true; break;
                case 2: lonPlus = true; break;
                case 3: latPlus = true; lonPlus = true; break;
                case 4: latPlus = true; break;
                case 5: latPlus = true; lonMinus = true; break;
                case 6: lonMinus = true; break;
                case 7: latMinus = true; lonMinus = true; break;
                default: break;
            }

            if (latPlus && latitude + 1 < BoardMetadata.BoardSize) latitude++;
            if (latMinus && latitude - 1 >= 0) latitude--;
            if (lonPlus && longitude + 1 < BoardMetadata.BoardSize) longitude++;
            if (lonMinus && longitude - 1 >= 0) longitude--;

            if (_world.Board.Zones[latitude, longitude] is Lava)
            {
                return new int[2] { _ant.Location.Latitude, _ant.Location.Longitude };
            }
            else if (0 <= latitude && latitude < BoardMetadata.BoardSize &&
                0 <= longitude && longitude < BoardMetadata.BoardSize)
            {
                return new int[2] { latitude, longitude };
            }

            return new int[2]
            {
                _ant.Location.Latitude,
                _ant.Location.Longitude
            };
        }
    }
}
