using System;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using System.Linq;
using Anthill.Ants;
using Anthill.Strategies.Actions;
using Anthill.Locations;

namespace Anthill.Actions
{
    [Serializable]
    public class EggsProduceStrategy : IActionStrategy
    {
        private static volatile EggsProduceStrategy instance;
        private static object syncRoot = new Object();

        private EggsProduceStrategy() { }

        public static EggsProduceStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new EggsProduceStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            int larvas = 1; // world.Random.Next(0, 10);
            var queen = (Queen) character;

            if (larvas == 0)
                return;

            world.Teams.ToList().ForEach(team =>
            {
                // Queen's Team
                if (team.Entities.Contains(character))
                {
                    for (int i = 0; i < larvas; i++)
                    {
                        team.Entities.Add(
                            new Larva(
                                new LarvaFactory(
                                    new NearLocationFactory(queen, world),
                                    queen
                                ),
                                queen
                            )
                        );
                    }
                }
            });
        }
    }
}
