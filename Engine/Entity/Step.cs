using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Map;
using Engine.Strategy;

namespace Engine.Entity
{
    [Serializable]
    public class Step : ICloneable
    {
        private readonly int _turn;
        public int Turn
        {
            get { return _turn; }
        }

        private readonly Location _location;
        public Location Location
        {
            get { return _location; }
        }

        private readonly IActionStrategy _actionStrategy;
        public IActionStrategy ActionStrategy
        {
            get { return _actionStrategy; }
        }

        private readonly ITimeStrategy _timeStrategy;
        public ITimeStrategy TimeStrategy
        {
            get { return _timeStrategy; }
        }

        public Step(int turn, Location location, 
            IActionStrategy actionStrategy, ITimeStrategy timeStrategy)
        {
            _turn = turn;
            _location = location;
            _actionStrategy = actionStrategy;
            _timeStrategy = timeStrategy;
        }

        public object Clone()
        {
            return new Step(_turn, (Location) _location.Clone(), _actionStrategy, _timeStrategy);
        }
    }
}
