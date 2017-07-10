using Anthill.Locations;
using AnthillUI;
using System;
using System.Windows;
using System.Windows.Controls;

namespace fourmilliereALIHM
{
    public partial class SimulationWindow : Window
    {
        private readonly GameBuilder _gameBuilder;

        public SimulationWindow()
        {
            _gameBuilder = new GameBuilder();
            InitializeComponent();
            DataContext = App.AnthillModel;
        }

        public void RunSimulation(object sender, RoutedEventArgs routedEventArgs)
        {
            _gameBuilder.boardSize(
                SimulationBoardSize.Text != "" 
                    ? Int32.Parse(SimulationBoardSize.Text)
                    : 12
            );

            _gameBuilder.queenNumber(
                SimulationQueens.Text != ""
                   ? Int32.Parse(SimulationQueens.Text)
                   : 1
            );

            ComboBoxItem typeItem = (ComboBoxItem) SimulationComboQueenStart.SelectedItem;
            string value = typeItem.Content.ToString();

            _gameBuilder.queenStartMethod(
                value.Equals("Border") 
                    ? LocationMethod.Border 
                    : LocationMethod.Random
            );

            _gameBuilder.foodNumber(
                SimulationFoods.Text != ""
                    ? Int32.Parse(SimulationFoods.Text)
                    : 10
            );

            App.ResetSimulation(
                _gameBuilder.build()
            );

            this.Close();
        }
    }
}
