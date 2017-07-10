using System;
using Engine.Entity;
using Engine.Map;
using System.Collections.Generic;
using System.Linq;

namespace Anthill.Zones
{
    [Serializable]
    public class Lava : Zone
    {
        public Lava() : base("Lava", 1000) {}

        public override void AffectedBy(World world, IEnumerable<Entity> entities)
        {
            entities.ToList().ForEach(entity => entity.Life = 0);
        }

        public override object Clone()
        {
            return new Lava();
        }
    }
}
