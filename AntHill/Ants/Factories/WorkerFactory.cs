using Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Map;
using Engine;

namespace Anthill.Ants
{
    public class WorkerFactory : EntityFactory
    {
        private readonly Location _location;

        public WorkerFactory(Location location)
        {
            _location = location;
        }

        public override int MakeLife()
        {
            return BoardMetadata.Random.Next(25, 50);
        }

        public override Location MakeLocation()
        {
            return new Location(_location.Latitude, _location.Longitude);
        }

        public override string MakeName()
        {
            return "Worker";
        }

        public override List<Step> MakeSteps()
        {
            return new List<Step>();
        }
    }
}
