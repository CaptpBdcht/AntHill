using System;
using Engine.Entity;
using Engine.Map;
using Anthill.Artefacts;
using System.Collections.Generic;
using System.Linq;

namespace Anthill.Zones
{
    [Serializable]
    public class Ground : Zone, ICloneable, IFridge
    {
        public List<Pheromone> Pheromones;

        public List<Miam> MiamList;

        public Ground() : base("Ground")
        {
            Pheromones = new List<Pheromone>();

            MiamList = new List<Miam>();
        }
        
        public override void AffectedBy(World world, IEnumerable<Entity> entities)
        {
            entities.ToList()
                .FindAll(entity => entity is Ant)
                .ForEach(ant => Pheromones.Add(new Pheromone(new PheromoneFactory(ant.Location))));

            Pheromones.ForEach(pheromone => pheromone.Resolve(world));
            Pheromones.RemoveAll(pheromone => pheromone.Life == 0);
        }

        public override object Clone()
        {
            return new Ground()
            {
                Pheromones = Pheromones.Select(pheromone => (Pheromone) pheromone.Clone()).ToList(),
                MiamList = MiamList.Select(miam => (Miam) miam.Clone()).ToList()
            };
        }

        public void DepositMiams(Miam miam)
        {
            MiamList.Add(miam);
        }

        public Miam GetMiams()
        {
            Miam randomMiam = MiamList.Find(miam => miam.Life > 0);

            MiamList.Remove(randomMiam);

            return randomMiam;
        }
    }
}
