using Engine.Map;
using System.Collections.Generic;
using Engine.Entity;
using Anthill.Ants;
using Anthill.Locations;
using Anthill.Zones;

namespace Anthill
{
    public class CustomAnthillFactory : WorldFactory
    {
        public int CustomBoardSize { get; set; }

        public int CustomQueenNumber { get; set; }

        public int CustomFoodNumber { get; set; }

        public LocationMethod CustomQueenStartMethod { get; set; }

        public CustomAnthillFactory(int boardSize, int queenNb, int foodNb, LocationMethod queenLocationMethod)
        {
            CustomBoardSize = boardSize;
            CustomQueenNumber = queenNb < 1 ? 1 : (queenNb > 5 ? 5 : queenNb);
            CustomFoodNumber = foodNb;
            CustomQueenStartMethod = queenLocationMethod;
        }

        public override Board MakeBoard()
        {
            return new Valhalla(CustomBoardSize, new ZoneFactory());
        }

        public override IEnumerable<Team> MakeTeams()
        {
            List<Team> teams = new List<Team>();

            for (int i = 1; i <= CustomQueenNumber; i++)
            {
                Team ants = new Team(i + ". Ants (" + GetTeamColor(i) + ")");
                Queen queen = new Queen(new QueenFactory(LocationMethodUtils.CorrespondingFactory(CustomQueenStartMethod)));
                queen.AddTurn(1);

                ants.Entities.Add(queen);
                teams.Add(ants);
            }

            Team food = new Team("Foods");
            for (int i = 0; i < CustomFoodNumber; i++)
            {
                Miam miam = new Miam(MiamFactory.Instance);
                miam.AddTurn(1);
                food.Entities.Add(miam);
            }
            teams.Add(food);

            foreach (var team in teams)
                yield return team;
        }

        private string GetTeamColor(int number)
        {
            switch(number)
            {
                case 1: return "Black";
                case 2: return "Blue";
                case 3: return "Red";
                case 4: return "Green";
                case 5: return "Orange";
                default: return "NOPE";
            }
        }
    }
}
