using Anthill;
using Anthill.Locations;
using System.Windows;

namespace AnthillUI
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AnthillModel AnthillModel { get; set; }

        public App()
        {
            AnthillModel = new AnthillModel( new Anthill.Anthill(
                (new CustomAnthillFactory(21, 3, 63, LocationMethod.Random))
            ));
        }

        public static void ResetSimulation(CustomAnthillFactory customAnthillFactory) 
        {
            Anthill.Anthill anthill = new Anthill.Anthill(customAnthillFactory);

            AnthillModel.Anthill = anthill;
            AnthillModel.CurrentWorld = anthill.GetWorld();
        }
    }
}
