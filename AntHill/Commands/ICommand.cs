using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anthill.Ants;
using Engine.Entity;

namespace Anthill.Commands
{
    public interface ICommand
    {
        void execute(Queen producer, Ant consummer);
    }
}
