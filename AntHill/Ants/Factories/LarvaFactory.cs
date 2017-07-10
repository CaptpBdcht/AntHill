using Engine.Entity;
using System;
using System.Collections.Generic;
using Engine.Map;
using Engine;

namespace Anthill.Ants
{
    public class LarvaFactory : EntityFactory
    {
        private readonly LocationFactory _locationFactory;
        private readonly Queen _queen;

        public LarvaFactory(LocationFactory locationFactory, Queen queen)
        {
            _locationFactory = locationFactory;
            _queen = queen;
        }

        public override int MakeLife()
        {
            return BoardMetadata.Random.Next(5, 10);
        }

        public override Location MakeLocation()
        {
            return new Location(_locationFactory);
        }

        public override string MakeName()
        {
            return "Larva";
        }

        public override List<Step> MakeSteps()
        {
            return new List<Step>();
        }
    }
}
