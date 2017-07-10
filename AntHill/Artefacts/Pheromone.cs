using Engine.Entity;
using Engine.Map;
using System.Collections.ObjectModel;
using Anthill.Strategies.Time;
using System;

namespace Anthill.Artefacts
{
    [Serializable]
    public class Pheromone : Entity
    {
        public Pheromone(EntityFactory entityFactory) : base(entityFactory)
        {
            TimeStrategy = GetOlderStrategy.Instance;
        }

        public Pheromone(string name, int life, Location location) : base(name, life, location)
        {
            TimeStrategy = GetOlderStrategy.Instance;
        }

        public override void Resolve(World world)
        {
            TimeStrategy.Endure(this, world);
        }

        protected override object Clone(ObservableCollection<Step> clonedSteps)
        {
            return new Pheromone(Name, Life, Location)
            {
                TimeStrategy = TimeStrategy
            };
        }
    }
}
