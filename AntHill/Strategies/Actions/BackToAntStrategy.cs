using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using Engine.Utils;

namespace Anthill.Strategies.Actions
{
    [Serializable]
    public class BackToAntStrategy : IActionStrategy
    {
        private readonly Ant _ant;

        public BackToAntStrategy(Ant ant)
        {
            _ant = ant;
        }

        public void Act(Character character, World world)
        {
            var locations = new List<Location> {_ant.Location};

            int moves = character is Ant
                      ? ((Ant)character).Speed
                      : 1;

            for (int i = 0; i < moves; i++)
                character.Location = new Dijkstra(world.Board).GetDijkstra(character.Location, locations);
        }
    }
}
