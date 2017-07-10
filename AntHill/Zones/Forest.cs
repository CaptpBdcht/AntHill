using System;
using Engine.Entity;
using Engine.Map;
using System.Collections.Generic;
using System.Linq;
using Anthill.Ants;

namespace Anthill.Zones
{
    [Serializable]
    public class Forest : Zone
    {
        public Forest() : base("Forest", 3) { }

        public override void AffectedBy(World world, IEnumerable<Entity> entities)
        {
            entities.ToList().ForEach(entity => { if (!(entity is Larva || entity is Queen)) entity.Life++; });
        }

        public override object Clone()
        {
            return new Forest();
        }
    }
}
