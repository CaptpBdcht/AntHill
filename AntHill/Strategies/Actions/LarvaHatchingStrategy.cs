using System;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using System.Linq;
using Anthill.Ants;
using Anthill.Strategies.Actions;
using Engine;

namespace Anthill.Actions
{
    [Serializable]
    public class LarvaHatchingStrategy : IActionStrategy
    {
        private static volatile LarvaHatchingStrategy instance;
        private static object syncRoot = new Object();

        private LarvaHatchingStrategy() { }

        public static LarvaHatchingStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LarvaHatchingStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            int rng = BoardMetadata.Random.Next(0, 10);

            world.Teams.ToList().ForEach(team =>
            {
                if (team.Entities.Contains(character))
                {
                    var ant = (Ant) character;
                    team.Entities.Remove(character);

                    if (rng != 0)
                        team.Entities.Add(new Worker(new WorkerFactory(character.Location), ant.Queen));
                    else
                        team.Entities.Add(new Warrior(new WarriorFactory(character.Location), ant.Queen));
                }
            });
        }
    }
}
