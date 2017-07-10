using System;
using Engine.Entity;
using Engine.Map;
using System.Collections.ObjectModel;
using Anthill.Strategies.Time;

namespace Anthill
{
    [Serializable]
    public class Miam : Entity
    {
        public int Value { get; set; }

        public Miam(EntityFactory entityFactory) : base(entityFactory)
        {
            Value = ((MiamFactory) entityFactory).MakeValue();
            TimeStrategy = GetOlderStrategy.Instance;
        }
        
        public Miam(string name, int life, Location location, int value) : base(name, life, location)
        {
            Value = value;
            TimeStrategy = GetOlderStrategy.Instance;
        }

        public override void Resolve(World world)
        {
            TimeStrategy.Endure(this, world);
        }

        protected override object Clone(ObservableCollection<Step> clonedSteps)
        {
            return new Miam(Name, Life, Location, Value)
            {
                Steps = clonedSteps,
                TimeStrategy = TimeStrategy
            };
        }
    }
}
