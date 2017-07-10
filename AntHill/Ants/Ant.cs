using System;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using Anthill.Ants;
using Anthill.Commands;

namespace Anthill
{
    [Serializable]
    public abstract class Ant : Character, IObserver
    {
        private Queen _queen;
        public Queen Queen
        {
            get
            {
                return _queen;
            }
            set
            {
                _queen = value;

                if (_queen != null)
                {
                    int turn = _queen.Steps[_queen.Steps.Count - 1].Turn;
                    if (this is Larva) turn++;
                    AddTurn(turn);
                    _queen.Subscribe(this);
                }
            }
        }

        public abstract int Speed { get; }
    
        public ICommand Command { get; set; }

        public Ant(EntityFactory entityFactory, Queen queen) : base(entityFactory)
        {
            Queen = queen;
        }

        public Ant(string name, int life, Location location, Queen queen) : base(name, life, location)
        {
            Queen = queen;
        }

        public abstract void ChooseTimeEffect();

        public abstract void ChooseAction(World world);

        public override sealed void Resolve(World world)
        {
            ChooseAction(world);
            ChooseTimeEffect();

            ActionStrategy.Act(this, world);
            TimeStrategy.Endure(this, world);
        }

        public abstract void Notify(Observable observable);
    }
}
