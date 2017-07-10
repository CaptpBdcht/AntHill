using Engine.Entity;
using System;
using System.Collections.Generic;
using Engine.Map;
using Engine;

namespace Anthill.Ants
{
    public class WarriorFactory : EntityFactory
    {
        private readonly Location _location;

        public WarriorFactory(Location location)
        {
            _location = location;
        }

        public override int MakeLife()
        {
            return BoardMetadata.Random.Next(100, 150);
        }

        public override Location MakeLocation()
        {
            return new Location(_location.Latitude, _location.Longitude);
        }

        public override string MakeName()
        {
            return "Warrior";
        }

        public override List<Step> MakeSteps()
        {
            return new List<Step>();
        }
    }
}
