using System;
using Engine.Map;

namespace Anthill.Zones
{
    public class ZoneFactory : IZoneFactory
    {
        public Ground MakeGround()
        {
            return new Ground();
        }

        public Lava MakeLava()
        {
            return new Lava();
        }

        public Forest MakeForest()
        {
            return new Forest();
        }
    }
}
