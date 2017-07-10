using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.Entity;
using System;
using System.Linq;

namespace Engine.Map
{
    [Serializable]
    public class World : ICloneable
    {
        public Board Board { get; set; }
        public ObservableCollection<Team> Teams { get; }

        public World(WorldFactory factory)
        {
            Board = factory.MakeBoard();
            Teams = new ObservableCollection<Team>(factory.MakeTeams());
        }

        public World(Board board, IEnumerable<Team> teams)
        {
            Board = board;
            Teams = new ObservableCollection<Team>(teams);
        }

        public object Clone()
        {
            ObservableCollection<Team> teams = new ObservableCollection<Team>();

            Teams.ToList().ForEach(team =>
            {
                teams.Add((Team) team.Clone());
            });

            return new World(
                (Board) Board.Clone(),
                teams
            );
        }

        public IEnumerable<Entity.Entity> EntitiesAt(Location location)
        {
            foreach (var team in Teams)
            {
                foreach (var entity in team.Entities)
                {
                    if (entity.Location.Equals(location))
                        yield return entity;
                }
            }
        }
    }
}