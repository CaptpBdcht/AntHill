using Engine.Map;
using Engine.Strategy;
using System;

namespace Engine.Entity
{
    [Serializable]
    public abstract class Character : Entity
    {
        public IActionStrategy ActionStrategy { get; set; }

        public Character(EntityFactory entityFactory) : base(entityFactory)
        {
        }

        public Character(string name, int life, Location location) : base(name, life, location)
        {
        }

        public override sealed void AddTurn(int turn)
        {
            Steps.Add(new Step(turn, Location, ActionStrategy, TimeStrategy));
        }
    }
}