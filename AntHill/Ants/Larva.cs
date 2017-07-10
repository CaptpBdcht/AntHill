using System;
using System.Collections.ObjectModel;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using Anthill.Actions;
using Anthill.Strategies.Time;

namespace Anthill.Ants
{
    [Serializable]
    public class Larva : Ant
    {
        public override int Speed => 0;

        public Larva(EntityFactory entityFactory, Queen queen) : base(entityFactory, queen) {}

        public Larva(string name, int life, Location location, Queen queen) : base(name, life, location, queen) {}

        public override void ChooseTimeEffect()
        {
            TimeStrategy = GetOlderStrategy.Instance;
        }

        public override void ChooseAction(World world)
        {
            if (Life == 1)
                ActionStrategy = LarvaHatchingStrategy.Instance;
            else
                ActionStrategy = DoNothingStrategy.Instance;
        }

        protected override object Clone(ObservableCollection<Step> clonedSteps)
        {
            return new Larva(Name, Life, Location, (Queen) Queen.Clone())
            {
                Steps = clonedSteps,
                TimeStrategy = TimeStrategy
            };
        }

        public override void Notify(Observable observable) {}
    }
}
