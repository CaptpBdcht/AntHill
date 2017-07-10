using System;
using System.Collections.Generic;

namespace Engine.Map
{
    [Serializable]
    public abstract class Zone : ICloneable
    {
        protected readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        protected readonly int _difficulty;
        public int Difficulty
        {
            get { return _difficulty; }
        }

        public Zone(string name, int difficulty = 1)
        {
            _name = name;
            _difficulty = difficulty;
        }

        public abstract void AffectedBy(World world, IEnumerable<Entity.Entity> entities);

        public abstract object Clone();
    }
}