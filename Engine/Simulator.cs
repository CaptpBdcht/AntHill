using System.Collections.Generic;
using Engine.Entity;
using Engine.Map;
using System.Linq;
using System;
using System.Threading;

namespace Engine
{
    [Serializable]
    public abstract class Simulator
    {
        public bool InReview { get; set; }
        public int Turn { get; set; }
        public int ExecSpeed { get; set; }
        private bool IsRunning { get; set; }

        private Dictionary<int, World> History { get; set; }

        protected Simulator(WorldFactory worldFactory)
        {
            IsRunning = false;
            Turn = 1; // First Turn

            History = new Dictionary<int, World>() {
                { Turn, new World(worldFactory) }
            };

            InReview = false; // Currently reviewing game (previous pressed - not last turn)
            ExecSpeed = 1000; // 1s
        }

        public World GetWorld()
        {
            return GetWorld(Turn);
        }

        private World GetWorld(int turn)
        {
            History.TryGetValue(turn, out World world);
            return world;
        }

        public World PreviousTurn()
        {
            if (Turn == 1)
                return GetWorld();

            InReview = true;
            --Turn;

            return GetWorld();
        }

        public World NextTurn()
        {
            ++Turn;

            if (GetWorld() != null)
                return GetWorld();

            History.Add(Turn, (World) GetWorld(Turn - 1).Clone());

            var currentWorld = GetWorld();
            HandleEntitiesTurn(currentWorld);
            HandleZonesTurn(currentWorld);

            InReview = false;
            return currentWorld;
        }

        private void HandleEntitiesTurn(World world)
        {
            world.Teams.ToList().ForEach(team => {
                team.Entities.ToList().ForEach(entity => {
                    entity.Resolve(world);
                    entity.AddTurn(Turn);
                });
            });
        }

        private void HandleZonesTurn(World world)
        {
            for (var i = 0; i < world.Board.Size; ++i)
            {
                for (var j = 0; j < world.Board.Size; ++j)
                {
                    var location = new Location(i, j);
                    var zone = world.Board.Get(location);
                    var entities = world.EntitiesAt(location);

                    zone.AffectedBy(world, entities);
                }
            }
        }

        public void Resume()
        {
            IsRunning = true;
            while (IsRunning)
            {
                Thread.Sleep(ExecSpeed);

                if (IsRunning)
                {
                    NextTurn();
                }
            }    
        }

        public World Pause()
        {
            IsRunning = false;
            return GetWorld();
        }

        public World Rewind()
        {
            Turn = 1;
            return GetWorld();
        }

        public abstract void Save(string path);

        public abstract Simulator Load(string path);
    }
}