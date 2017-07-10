using System;
using Engine;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;
using Anthill.Zones;

namespace Anthill.Actions
{
    [Serializable]
    public class SimpleMoveStrategy : IActionStrategy
    {
        private static volatile SimpleMoveStrategy instance;
        private static object syncRoot = new Object();

        private SimpleMoveStrategy() { }

        public static SimpleMoveStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SimpleMoveStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            int moves = character is Ant
                      ? ((Ant)character).Speed
                      : 1;

            for (int i = 0; i < moves; i++)
            {
                int variable = BoardMetadata.Random.Next(0, 4);
                int latitude = character.Location.Latitude;
                int longitude = character.Location.Longitude;

                switch (variable)
                {
                    case 0: if (latitude != 0) latitude--; break; // Up
                    case 1: if (latitude != world.Board.Size - 1) latitude++; break; // Down
                    case 2: if (longitude != 0) longitude--; break; // Left
                    case 3: if (longitude != world.Board.Size - 1) longitude++; break; // Right
                    default: break; // Don't Move
                }

                if (!(world.Board.Zones[latitude, longitude] is Lava))
                    character.Location = new Location(latitude, longitude);
            }
        }
    }
}
