using System;
using Anthill.Ants;
using Anthill.Strategies.Actions;

namespace Anthill.Commands
{
    [Serializable]
    public class BackToQueenCommand : ICommand
    {
        public void execute(Queen producer, Ant consummer)
        {
            consummer.ActionStrategy = new BackToAntStrategy(producer);
        }
    }
}
