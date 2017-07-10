using Engine.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Entity;
using Engine.Map;
using Anthill.Zones;
using Anthill.Ants;

namespace Anthill.Strategies.Actions
{
    class StoreFoodStrategy : IActionStrategy
    {
        private static volatile StoreFoodStrategy instance;
        private static object syncRoot = new Object();

        private StoreFoodStrategy() { }

        public static StoreFoodStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new StoreFoodStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
            if (world.Board.Get(character.Location) is IFridge iFridge
                && character is Worker worker)
            {
                Console.WriteLine("Coucou je m'appelle " + worker.Name + " et j'ai deposit " + worker.StockFood + " miams");
                iFridge.DepositMiams(new Miam(worker.Name, -1, worker.Location, worker.StockFood));
                worker.StockFood = 0;
            }
        }
    }
}
