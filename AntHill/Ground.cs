using System;
using Engine.Map;

namespace Anthill
{
    [Serializable]
    public class Ground : Zone, ICloneable
    {
        public override object Clone()
        {
            return new Ground();
        }
    }
}
