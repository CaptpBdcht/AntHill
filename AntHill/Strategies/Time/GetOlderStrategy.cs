using Engine.Strategy;
using System;
using System.Linq;
using Engine.Entity;
using Engine.Map;

namespace Anthill.Strategies.Time
{
    [Serializable]
    public class GetOlderStrategy : ITimeStrategy
    {
        private static volatile GetOlderStrategy instance;
        private static object syncRoot = new Object();

        private GetOlderStrategy() {}

        public static GetOlderStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GetOlderStrategy();
                    }
                }

                return instance;
            }
        }

        public void Endure(Entity entity, World world)
        {
            if (--entity.Life <= 0)
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
