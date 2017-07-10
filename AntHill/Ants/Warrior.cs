using System;
using System.Collections.ObjectModel;
using Engine.Entity;
using Engine.Map;
using Anthill.Strategies.Time;
using Anthill.Actions;

namespace Anthill.Ants
{
    [Serializable]
    public class Warrior : Ant
    {
        public override int Speed => 2;

        public Warrior(EntityFactory entityFactory, Queen queen) : base(entityFactory, queen) {}

        public Warrior(string name, int life, Location location, Queen queen) : base(name, life, location, queen) {}

        public override void ChooseTimeEffect()
        {
            TimeStrategy = GetOlderStrategy.Instance;
        }

        public override void ChooseAction(World world)
        {
            ActionStrategy = SimpleMoveStrategy.Instance;
        }

        protected override object Clone(ObservableCollection<Step> clonedSteps)
        {
            return new Warrior(Name, Life, Location, (Queen) Queen.Clone())
            {
                Steps = clonedSteps,
                TimeStrategy = TimeStrategy
            };
        }

        public override void Notify(Observable observable) {}
    }
}
