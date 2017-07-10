using System;
using System.Collections.ObjectModel;
using Engine.Entity;
using Engine.Map;
using Anthill.Actions;
using Anthill.Commands;
using Anthill.Strategies.Time;
using Engine;

namespace Anthill.Ants
{
    [Serializable]
    public class Queen : Ant
    {
        private volatile Queen clonedInstance;
        private object syncRoot = new Object();

        public override int Speed => 1;

        public Queen(QueenFactory queenFactory) : base(queenFactory, null) {} 

        public Queen(string name, int life, Location location) : base(name, life, location, null) {}

        public override void ChooseTimeEffect()
        {
            TimeStrategy = GetOlderStrategy.Instance;
        }

        public override void ChooseAction(World world)
        {
            if (BoardMetadata.Random.Next(0, 10) == 0)
            {
                switch (BoardMetadata.Random.Next(0, 3))
                {
                    case 0:
                        Command = null;
                        break;
                    case 1:
                        Command = new BackToQueenCommand();
                        break;
                    case 2:
                        Command = new IdleCommand();
                        break;
                }

                NotifyAll();
            }

            ActionStrategy = EggsProduceStrategy.Instance;
        }

        protected override object Clone(ObservableCollection<Step> clonedSteps)
        {
            if (clonedInstance == null)
            {
                lock (syncRoot)
                {
                    if (clonedInstance == null)
                    {
                        clonedInstance = new Queen(Name, Life, Location)
                        {
                            Steps = clonedSteps,
                            TimeStrategy = TimeStrategy
                        };
                    }
                }
            }

            return clonedInstance;
        }

        public override void Notify(Observable observable) {}
    }
}
