using Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Map
{
    public abstract class WorldFactory
    {
        public abstract Board MakeBoard();

        public abstract IEnumerable<Team> MakeTeams();
    }
}
