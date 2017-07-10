using System;
using System.Collections.Generic;
using Engine.Map;

namespace Engine.Entity
{
    public abstract class EntityFactory
    {
        public abstract string MakeName();

        public abstract int MakeLife();

        public abstract List<Step> MakeSteps();

        public abstract Location MakeLocation();
    }
}