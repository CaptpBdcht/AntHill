using Engine;
using Engine.Map;
using System;

namespace Anthill.Locations
{
    public class RandomLocationFactory : LocationFactory
    {
        private static volatile RandomLocationFactory instance;
        private static object syncRoot = new Object();

        private RandomLocationFactory() { }

        public static RandomLocationFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RandomLocationFactory();
                    }
                }

                return instance;
            }
        }

        public override int[] MakeCoordinates()
        {
            return new int[2]
            {
                BoardMetadata.Random.Next(0, BoardMetadata.BoardSize),
                BoardMetadata.Random.Next(0, BoardMetadata.BoardSize)
            };
        }
    }
}
