using System;
using Engine.Map;

namespace Anthill
{
    public class GroundFactory : IZoneFactory
    {
        public Ground MakeGround()
        {
            return new Ground();
        }
    }
}
