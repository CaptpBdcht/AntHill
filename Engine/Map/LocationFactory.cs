using System;

namespace Engine.Map
{
    public abstract class LocationFactory
    {
        public abstract int[] MakeCoordinates();
    }
}