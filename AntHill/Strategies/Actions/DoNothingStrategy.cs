using System;
using Engine.Entity;
using Engine.Map;
using Engine.Strategy;

namespace Anthill.Actions
{
    [Serializable]
    public class DoNothingStrategy : IActionStrategy
    {
        private static volatile DoNothingStrategy instance;
        private static object syncRoot = new Object();

        private DoNothingStrategy() { }

        public static DoNothingStrategy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DoNothingStrategy();
                    }
                }

                return instance;
            }
        }

        public void Act(Character character, World world)
        {
        }
    }
}
