using Engine;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using System;
using System.Linq;

namespace Anthill.Strategies.Time
{
   [Serializable]
    public class LittleIllnessStrategy : ITimeStrategy
    {
        private static volatile LittleIllnessStrategy instance;
        private static object syncRoot = new Object();

        private LittleIllnessStrategy() { }

        public static LittleIllnessStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LittleIllnessStrategy();
                    }
                }

                return instance;
            }
        }

        public void Endure(Entity entity, World world)
        {
            int pain = BoardMetadata.Random.Next(3, 8);
            entity.Life -= pain;

            if (entity.Life <= 0)
            {
                world.Teams.ToList().ForEach(team =>
                {
                    if (team.Entities.Contains(entity))
                        team.Entities.Remove(entity);
                });
            }
        }
    }
}
