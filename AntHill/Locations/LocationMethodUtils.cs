using Engine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anthill.Locations
{
    public static class LocationMethodUtils
    {
        public static LocationFactory CorrespondingFactory(LocationMethod method)
        {
            switch (method)
            {
                case LocationMethod.Border:
                    return BorderLocationFactory.Instance;
                case LocationMethod.Random:
                default:
                    return RandomLocationFactory.Instance;
            }
        }
    }
}
