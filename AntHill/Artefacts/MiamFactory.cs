using System.Collections.Generic;
using Engine.Entity;
using Engine.Map;
using System;
using Engine;

namespace Anthill
{
    public class MiamFactory : EntityFactory
    {
        public static volatile MiamFactory instance;
        public static object syncRoot = new Object();

        private MiamFactory() {}

        public static MiamFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MiamFactory();
                    }
                }

                return instance;
            }
        }

        public override int MakeLife()
        {
            return 500;
        }

        public override Location MakeLocation()
        {
            return new Location(BoardMetadata.Random.Next(0, BoardMetadata.BoardSize), 
                BoardMetadata.Random.Next(0, BoardMetadata.BoardSize));
        }

        public override string MakeName()
        {
            return "Miam";
        }

        public override List<Step> MakeSteps()
        {
            return new List<Step>();
        }

        public int MakeValue()
        {
            int value = BoardMetadata.Random.Next(10, 50);
            return value;
        }
    }
}
