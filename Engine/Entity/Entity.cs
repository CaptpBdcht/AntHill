using System.Collections.ObjectModel;
using Engine.Map;
using Engine.Strategy;
using System.Linq;
using System;

namespace Engine.Entity
{
    [Serializable]
    public abstract class Entity : Observable, ICloneable
    {
        public string Name { get; set; }

        public int Life { get; set; }

        public ObservableCollection<Step> Steps { get; set; }

        public Location Location { get; set; }

        public ITimeStrategy TimeStrategy { get; set; }

        public Entity(EntityFactory entityFactory)
        {
            Name = entityFactory.MakeName();
            Life = entityFactory.MakeLife();
            Steps = new ObservableCollection<Step>(entityFactory.MakeSteps());
            Location = entityFactory.MakeLocation();
        }

        public Entity(string name, int life, Location location)
        {
            Name = name;
            Life = life;
            Steps = new ObservableCollection<Step>();
            Location = location;
        }

        // Handles IA logic to choose 
        public abstract void Resolve(World world);

        public object Clone()
        {
            ObservableCollection<Step> steps = new ObservableCollection<Step>();
            Steps.ToList().ForEach(step => steps.Add((Step) step.Clone()));

            return Clone(steps);
        }

        protected abstract object Clone(ObservableCollection<Step> clonedSteps);

        public virtual void AddTurn(int turn)
        {
            Steps.Add(new Step(turn, Location, null, TimeStrategy));
        }
    }
}