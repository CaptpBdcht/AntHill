using Engine.Strategy;
using System;
using System.Collections.Generic;
using Engine.Entity;
using Engine.Map;
using Engine;
using Anthill.Zones;
using Anthill.Artefacts;

namespace Anthill.Strategies.Actions
{
    [Serializable]
    class FollowPheromonesStrategy : IActionStrategy
    {
        private static volatile FollowPheromonesStrategy instance;
        private static object syncRoot = new Object();

        private FollowPheromonesStrategy() { }

        public static FollowPheromonesStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FollowPheromonesStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            double[,] distances = new double[BoardMetadata.BoardSize, BoardMetadata.BoardSize];

            Location bestLocation = new Location(character.Location.Latitude, character.Location.Longitude);
            double bestDistance = 1000000;

            for (int i = 0; i < BoardMetadata.BoardSize; i++)
            {
                for (int j = 0; j < BoardMetadata.BoardSize; j++)
                {
                    if (world.Board.Zones[i, j] is Ground ground
                        && i != character.Location.Latitude
                        && j != character.Location.Longitude)
                    {
                        int pheromones = ground.Pheromones.Count;

                        if (pheromones == 0)
                            distances[i, j] = -1;
                        else
                        {
                            distances[i, j] = pheromones / character.Location.AbsoluteDistanceFrom(new Location(i, j));
                            if (distances[i, j] > bestDistance)
                            {
                                bestDistance = distances[i, j];
                                bestLocation = new Location(i, j);
                            }
                        }
                    }
                }
            }

            int moves = character is Ant
                      ? ((Ant)character).Speed
                      : 1;

            for (int i = 0; i < moves; i++)
                character.Location = MoveForwardTo(character.Location, bestLocation);
        }

        private Location MoveForwardTo(Location from, Location to)
        {
            if (from.Equals(to))
                return from;

            int xDistance = from.Latitude - to.Latitude;
            int yDistance = from.Longitude - to.Longitude;

            if (xDistance < 0)
                return new Location(from.Latitude + 1, from.Longitude);
            else if (xDistance > 0)
                return new Location(from.Latitude - 1, from.Longitude);

            if (yDistance < 0)
                return new Location(from.Latitude, from.Longitude + 1);
            else if (yDistance > 0)
                return new Location(from.Latitude, from.Longitude - 1);

            return from;
        }
    }
}
