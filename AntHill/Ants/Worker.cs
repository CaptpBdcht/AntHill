using System;
using System.Collections.ObjectModel;
using Engine.Entity;
using Engine.Map;
using Anthill.Strategies.Time;
using Anthill.Strategies.Actions;
using System.Collections.Generic;
using Engine;

namespace Anthill.Ants
{
    [Serializable]
    public class Worker : Ant
    {
        public override int Speed => 3;

        public int MaxStock => BoardMetadata.Random.Next(12, 43);

        private int _stockFood;
        public int StockFood {
            get
            {
                return _stockFood;
            }
            set
            {
                _stockFood = value;
            }
        }

        public Worker(EntityFactory entityFactory, Queen queen) : base(entityFactory, queen) {
            StockFood = 0;
        }

        public Worker(string name, int life, Location location, Queen queen) : base(name, life, location, queen) {
            StockFood = 0;
        }

        public override void ChooseAction(World world)
        {
            if (Command != null)
            {
                Command.execute(Queen, this);
            }
            else if (StockFood >= MaxStock)
            {
                if (Location.Equals(Queen.Location))
                    ActionStrategy = StoreFoodStrategy.Instance;
                else
                    ActionStrategy = new BackToAntStrategy(Queen);
            }
            else
            {
                List<Entity> hereEntities = new List<Entity>(world.EntitiesAt(Location));

                if (hereEntities.Find(entity => entity is Miam) != null)
                {
                    if (BoardMetadata.Random.Next(0, 5) == 0)
                        ActionStrategy = FollowPheromonesStrategy.Instance;
                    else
                        ActionStrategy = EatFoodStrategy.Instance;
                }
                else
                    ActionStrategy = FindFoodStrategy.Instance;
            }
        }

        public override void ChooseTimeEffect()
        {
            TimeStrategy = GetOlderStrategy.Instance;
        }

        protected override object Clone(ObservableCollection<Step> clonedSteps)
        {
            return new Worker(Name, Life, Location, (Queen) Queen.Clone())
            {
                Steps = clonedSteps,
                Command = Command,
                TimeStrategy = TimeStrategy,
                StockFood = _stockFood
            };
        }

        public override void Notify(Observable observable)
        {
            Command = Queen.Command;
        }
    }
}
