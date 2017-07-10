using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anthill;
using Anthill.Locations;
using Engine.Map;

namespace fourmilliereALIHM
{
    public class GameBuilder
    {
        public int BoardSize { get; set; }

        public int QueenNumber { get; set; }

        public int FoodNumber { get; set; }

        public LocationMethod QueenStartMethod { get; set; }

        public GameBuilder boardSize(int boardSize)
        {
            BoardSize = boardSize;
            return this;
        }

        public GameBuilder queenNumber(int queenNumber)
        {
            QueenNumber = queenNumber;
            return this;
        }

        public GameBuilder foodNumber(int foodNumber)
        {
            FoodNumber = foodNumber;
            return this;
        }

        public GameBuilder queenStartMethod(LocationMethod queenStartMethod)
        {
            QueenStartMethod = queenStartMethod;
            return this;
        }

        public CustomAnthillFactory build()
        {
            return new CustomAnthillFactory(BoardSize, QueenNumber, FoodNumber, QueenStartMethod);
        }
    }
}
