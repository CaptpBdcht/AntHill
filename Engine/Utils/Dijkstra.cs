using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Map;

namespace Engine.Utils
{
    public class Dijkstra
    {
        private readonly Board _board;

        public Dijkstra(Board board)
        {
            _board = board;
        }

        public Location GetDijkstra(Location source, List<Location> entities)
        {
            return entities.Count > 0
                    ? FindShortestLocation(source, NearestEntity(source, FindWeightsFrom(source), entities)).ToLocation()
                    : source;
        }

        private DijkstraLocation NearestEntity(Location source, DijkstraLocation[,] weights, List<Location> entities)
        {
            DijkstraLocation nearest = null;
            int nearestValue = int.MaxValue;

            entities.ForEach(entity =>
            {
                if (weights[entity.Latitude, entity.Longitude].Weight < nearestValue)
                {
                    nearest = weights[entity.Latitude, entity.Longitude];
                    nearestValue = weights[entity.Latitude, entity.Longitude].Weight;
                }
            });
            
            return nearest;
        }

        private DijkstraLocation[,] FindWeightsFrom(Location source)
        {
            DijkstraLocation[,] locations = InitLocations(source);
            bool[,] visited = InitVisited();

            int vertices = _board.Size * _board.Size - 1;

            for (int i = 0; i < vertices; i++)
            {
                DijkstraLocation current = GetUnvisitedLowerValueLocation(locations, visited);
                visited[current.Latitude, current.Longitude] = true;

                foreach (DijkstraLocation location in GetUnvisitedNeighbours(current, locations, visited))
                {
                    int locationWeight = locations[location.Latitude, location.Longitude].Weight;
                    int difficulty = _board.Zones[location.Latitude, location.Longitude].Difficulty;
                    int newWeight = difficulty + current.Weight;

                    if (newWeight < locationWeight)
                    {
                        locations[location.Latitude, location.Longitude].Predecessor = current;
                        locations[location.Latitude, location.Longitude].Weight = newWeight;
                    }
                }
            }

            return locations;
        }

        private DijkstraLocation FindShortestLocation(Location source, DijkstraLocation target)
        {
            DijkstraLocation[,] locations = InitLocations(source);
            bool[,] visited = InitVisited();

            int vertices = _board.Size * _board.Size;

            for (int i = 0; i < vertices; i++)
            {
                DijkstraLocation current = GetUnvisitedLowerValueLocation(locations, visited);
                visited[current.Latitude, current.Longitude] = true;

                foreach (DijkstraLocation location in GetUnvisitedNeighbours(current, locations, visited))
                {
                    int locationWeight = locations[location.Latitude, location.Longitude].Weight;
                    int difficulty = _board.Zones[location.Latitude, location.Longitude].Difficulty;
                    int newWeight = difficulty + current.Weight;

                    if (newWeight < locationWeight)
                    {
                        locationWeight = newWeight;
                        locations[location.Latitude, location.Longitude].Predecessor = current;
                        locations[location.Latitude, location.Longitude].Weight = newWeight;
                    }
                }
            }

            return FindNearestOrigin(locations, target, source);
        }

        private DijkstraLocation[,] InitLocations(Location source)
        {
            int length = _board.Size;
            DijkstraLocation[,] locations = new DijkstraLocation[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    locations[i, j] = new DijkstraLocation(i, j, null);
                }
            }

            locations[source.Latitude, source.Longitude].Weight = 0;

            return locations;
        }

        private bool[,] InitVisited()
        {
            int length = _board.Size;
            bool[,] visited = new bool[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    visited[i, j] = false;
                }
            }

            return visited;
        }

        private DijkstraLocation GetUnvisitedLowerValueLocation(DijkstraLocation[,] locations, bool[,] visited)
        {
            DijkstraLocation location = null;

            int length = _board.Size;
            int minimum = int.MaxValue;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (!visited[i, j] && locations[i, j].Weight < minimum)
                    {
                        minimum = locations[i, j].Weight;
                        location = new DijkstraLocation(i, j, locations[i, j].Predecessor)
                        {
                            Weight = minimum
                        };
                    }
                }
            }

            return location;
        }

        private List<DijkstraLocation> GetUnvisitedNeighbours(DijkstraLocation current, DijkstraLocation[,] locations, bool[,] visited)
        {
            List<DijkstraLocation> neighbors = new List<DijkstraLocation>();

            if (current.Latitude > 0 && !visited[current.Latitude - 1, current.Longitude])
                neighbors.Add(new DijkstraLocation(current.Latitude - 1, current.Longitude, current.Predecessor));

            if (current.Latitude < _board.Size - 1 && !visited[current.Latitude + 1, current.Longitude])
                neighbors.Add(new DijkstraLocation(current.Latitude + 1, current.Longitude, current.Predecessor));

            if (current.Longitude > 0 && !visited[current.Latitude, current.Longitude - 1])
                neighbors.Add(new DijkstraLocation(current.Latitude, current.Longitude - 1, current.Predecessor));

            if (current.Longitude < _board.Size - 1 && !visited[current.Latitude, current.Longitude + 1])
                neighbors.Add(new DijkstraLocation(current.Latitude, current.Longitude + 1, current.Predecessor));

            return neighbors;
        }

        private DijkstraLocation FindNearestOrigin(DijkstraLocation[,] locations, DijkstraLocation target, Location source)
        {
            if (target.Weight == 0)
                return target;

            DijkstraLocation dijkstraTarget = target;
            DijkstraLocation dijkstraSource = locations[source.Latitude, source.Longitude];

            while (!dijkstraSource.Equals(dijkstraTarget.Predecessor))
                dijkstraTarget = dijkstraTarget.Predecessor;

            return dijkstraTarget;
        }
    }
}
