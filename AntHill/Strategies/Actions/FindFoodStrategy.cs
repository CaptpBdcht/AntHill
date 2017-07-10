using Engine.Map;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Entity;
using Engine.Strategy;

namespace Anthill.Strategies.Actions
{
    [Serializable]
    class FindFoodStrategy : IActionStrategy
    {
        private static volatile FindFoodStrategy instance;
        private static object syncRoot = new Object();

        private FindFoodStrategy() { }

        public static FindFoodStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FindFoodStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            List<Location> locations = new List<Location>();

            world.Teams.ToList().ForEach(team =>
            {
                team.Entities.ToList().ForEach(entity =>
                {
                    if (entity is Miam)
                        locations.Add(entity.Location);
                });
            });

            int moves = character is Ant ant
                      ? ant.Speed
                      : 1;
            
            for (int i = 0; i < moves; i++)
                character.Location = new Dijkstra(world.Board).GetDijkstra(character.Location, locations);
        }
    }
}
