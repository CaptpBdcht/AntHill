using Engine.Entity;
using Engine.Map;

namespace AnthillUI
{
    public class AnthillModel
    {
        private readonly string _applicationTitle;
        public string ApplicationTitle
        {
            get { return _applicationTitle; }
        }

        public Anthill.Anthill Anthill { get; set; }
        public World CurrentWorld { get; set; }
        public Team SelectedTeam { get; set; }
        public Entity SelectedEntity { get; set; }

        public AnthillModel(Anthill.Anthill anthill)
        {
            _applicationTitle = "Anthill Simulation";

            Anthill = anthill;
            CurrentWorld = Anthill.GetWorld();
        }
    }
}
