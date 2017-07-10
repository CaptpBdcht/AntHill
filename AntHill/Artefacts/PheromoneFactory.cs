using Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Map;

namespace Anthill.Artefacts
{
    public class PheromoneFactory : EntityFactory
    {
        private readonly Location _location;

        public PheromoneFactory(Location location)
        {
            _location = location;
        }

        public override int MakeLife()
        {
            return 10;
        }

        public override Location MakeLocation()
        {
            return _location;
        }

        public override string MakeName()
        {
            return "Pheromone";
        }

        public override List<Step> MakeSteps()
        {
            return new List<Step>();
        }
    }
}
