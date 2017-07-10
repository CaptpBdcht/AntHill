using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Entity
{
    [Serializable]
    public class Team : ICloneable
    {
        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        private readonly ObservableCollection<Entity> _entities;
        public ObservableCollection<Entity> Entities
        {
            get { return _entities; }
        }

        public Team(string name)
        {
            _name = name;
            _entities = new ObservableCollection<Entity>();
        }

        private Team(string name, ObservableCollection<Entity> entities)
        {
            _name = name;
            _entities = entities;
        }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            ObservableCollection<Entity> entities = new ObservableCollection<Entity>();

            _entities.ToList().ForEach(entity =>
            {
                entities.Add((Entity) entity.Clone());
            });

            return new Team(Name, entities);
        }

        public void ToList()
        {
            throw new NotImplementedException();
        }
    }
}