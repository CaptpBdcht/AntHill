using Engine.Entity;
using System.Collections.Generic;
using Engine.Map;

namespace Anthill.Ants
{
    public class QueenFactory : EntityFactory
    {
        private readonly LocationFactory _locationFactory;

        public QueenFactory(LocationFactory locationFactory)
        {
            _locationFactory = locationFactory;
        }

        public override int MakeLife()
        {
            return 1000;
        }

        public override Location MakeLocation()
        {
            return new Location(_locationFactory);
        }

        public override string MakeName()
        {
            return "Queen";
        }

        public override List<Step> MakeSteps()
        {
            return new List<Step>();
        }
    }
}
