using System;
using Anthill.Actions;
using Anthill.Ants;

namespace Anthill.Commands
{
    [Serializable]
    public class IdleCommand : ICommand
    {
        public void execute(Queen producer, Ant consummer)
        {
            consummer.ActionStrategy = DoNothingStrategy.Instance;
        }
    }
}
