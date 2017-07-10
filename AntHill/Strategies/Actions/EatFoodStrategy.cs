using System;
using System.Linq;
using Engine.Entity;
using Engine;
using Engine.Map;
using Engine.Strategy;
using Anthill.Ants;

namespace Anthill.Strategies.Actions
{
    [Serializable]
    public class EatFoodStrategy : IActionStrategy
    {
        private static volatile EatFoodStrategy instance;
        private static object syncRoot = new Object();

        private EatFoodStrategy() { }

        public static EatFoodStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new EatFoodStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            int foodEaten = BoardMetadata.Random.Next(1, 10);

            world.Teams.ToList().ForEach(team =>
            {
                Entity food = team.Entities.ToList().Find(entity => entity.Location.Equals(character.Location));
                if (food is Miam miam)
                {
                    if (miam.Value < foodEaten)
                    {
                        foodEaten = miam.Value;
                        team.Entities.Remove(food);
                    }
                    else
                        miam.Value -= foodEaten;

                    character.Life += foodEaten;
                    if (character is Worker worker)
                    {
                        worker.StockFood += BoardMetadata.Random.Next(1, 4);
                    }
                }
            });
        }
    }
}
