using Engine;
using Engine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anthill.Locations
{
    public class BorderLocationFactory : LocationFactory
    {
        private static volatile BorderLocationFactory instance;
        private static object syncRoot = new Object();

        private BorderLocationFactory() { }

        public static BorderLocationFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BorderLocationFactory();
                    }
                }

                return instance;
            }
        }

        public override int[] MakeCoordinates()
        {
            int choice = BoardMetadata.Random.Next(0, 4);
            int latFixed = -1;
            int lonFixed = -1;

            switch (choice)
            {
                case 0: latFixed = 0; break;
                case 1: latFixed = BoardMetadata.BoardSize; break;
                case 2: lonFixed = 0; break;
                case 3: lonFixed = BoardMetadata.BoardSize; break;
                default: break;
            }

            if (latFixed == -1) latFixed = BoardMetadata.Random.Next(0, BoardMetadata.BoardSize);
            if (lonFixed == -1) lonFixed = BoardMetadata.Random.Next(0, BoardMetadata.BoardSize);

            return new int[2] { latFixed, lonFixed };
        }
    }
}
