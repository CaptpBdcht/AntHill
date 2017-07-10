using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using Engine.Map;
using Anthill.Actions;
using Anthill.Ants;
using Anthill;
using Engine.Entity;
using fourmilliereALIHM;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using Engine;
using Anthill.Zones;

namespace AnthillUI
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer dt = new DispatcherTimer();
        private Stopwatch stopWatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.AnthillModel;

            dt.Tick += Redraw;
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.AnthillModel.Anthill.ExecSpeed);

            Draw();
        }

        private void Redraw(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                World world = App.AnthillModel.Anthill.GetWorld();

                if (world == null)
                    return;

                App.AnthillModel.CurrentWorld = world;

                ResetBindings(App.AnthillModel.CurrentWorld);
                Draw();
            }
        }

        public void Draw()
        {
            InitBoard();
            DrawZones();
            DrawEntities();

            App.AnthillModel.SelectedTeam = App.AnthillModel.Anthill.GetWorld().Teams[0];
        }

        private void InitBoard()
        {
            Board.ColumnDefinitions.Clear();
            Board.RowDefinitions.Clear();
            Board.Children.Clear();

            int size = App.AnthillModel.CurrentWorld.Board.Size;
            for (int i = 0; i < size; i++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < size; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void DrawZones()
        {
            var board = App.AnthillModel.CurrentWorld.Board;

            for (var i = 0; i < board.Size; ++i)
            {
                for (var j = 0; j < board.Size; ++j)
                {
                    var location = new Location(i, j);
                    var zone = board.Get(location);

                    Image texture;

                    if (zone.Name == "Ground")
                    {
                        var ground = (Ground) zone;

                        if (ground.Pheromones.Count > 0 && ground.Pheromones.Count <= 10)
                            texture = WindowUtils.FindImage("light_pheromone");
                        else if (ground.Pheromones.Count > 10)
                            texture = WindowUtils.FindImage("heavy_pheromone");
                        else
                            continue;
                    }
                    else
                        texture = WindowUtils.FindZoneImage(zone);

                    Board.Children.Add(texture);
                    Grid.SetColumn(texture, location.Latitude);
                    Grid.SetRow(texture, location.Longitude);
                }
            }
        }

        private void DrawEntities()
        {
            foreach (var team in App.AnthillModel.CurrentWorld.Teams)
            {
                foreach (var entity in team.Entities)
                {
                    var img = WindowUtils.FindEntityImage(entity, team);

                    Board.Children.Add(img);
                    Grid.SetColumn(img, entity.Location.Latitude);
                    Grid.SetRow(img, entity.Location.Longitude);
                }
            }
        }

        public void PreviousTurn(object sender, RoutedEventArgs routedEventArgs)
        {
            World world = App.AnthillModel.Anthill.PreviousTurn();
            App.AnthillModel.CurrentWorld = world;

            ResetBindings(world);
            Draw();
        }

        public void NextTurn(object sender, RoutedEventArgs routedEventArgs)
        {
            World world = App.AnthillModel.Anthill.NextTurn();
            App.AnthillModel.CurrentWorld = world;

            ResetBindings(world);
            Draw();
        }

        public void Pause(object sender, RoutedEventArgs routedEventArgs)
        {
            App.AnthillModel.Anthill.Pause();
            Redraw(null, null);

            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                dt.Stop();
            }
        }

        public void Rewind(object sender, RoutedEventArgs routedEventArgs)
        {
            Pause(null, null);

            World world = App.AnthillModel.Anthill.Rewind();
            App.AnthillModel.CurrentWorld = world;

            ResetBindings(world);
            Draw();
        }

        public void Resume(object sender, RoutedEventArgs routedEventArgs)
        {
            if (stopWatch.IsRunning)
                return;

            stopWatch.Start();
            dt.Start();

            Thread tt = new Thread(App.AnthillModel.Anthill.Resume);
            tt.Start();
        }

        public void OnNew(object sender, RoutedEventArgs routedEventArgs)
        {
            Pause(null, null);

            SimulationWindow window = new SimulationWindow();
            window.ShowDialog();

            World world = App.AnthillModel.CurrentWorld;

            ResetBindings(world);
            Draw();
        }

        public void OnSave(object sender, RoutedEventArgs routedEventArgs)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = folderDialog.ShowDialog();

            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = folderDialog.SelectedPath;
                    App.AnthillModel.Anthill.Save(file);
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
        }

        public void OnLoad(object sender, RoutedEventArgs routedEventArgs)
        {
            Pause(null, null);

            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            var result = fileDialog.ShowDialog();

            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = fileDialog.FileName;

                    Anthill.Anthill anthill = (Anthill.Anthill)App.AnthillModel.Anthill.Load(file);
                    App.AnthillModel.Anthill = anthill;
                    App.AnthillModel.CurrentWorld = anthill.GetWorld();

                    ResetBindings(App.AnthillModel.CurrentWorld);
                    Draw();
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
        }

        public void OnExit(object sender, RoutedEventArgs routedEventArgs)
        {
            Pause(null, null);
            Application.Current.Shutdown();
        }

        private void ResetBindings(World world)
        {
            App.AnthillModel.SelectedEntity = null;
            App.AnthillModel.SelectedTeam = null;

            ComboSelectedTeam.SelectedIndex = -1;
            ComboSelectedTeam.ItemsSource = world.Teams;

            CellListBox.ItemsSource = null;
            ClickedCellName.Text = "";
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(Board);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in Board.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in Board.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            // row and col now correspond Grid's RowDefinition and ColumnDefinition
            List<Entity> cellEntities = new List<Entity>(App.AnthillModel.Anthill.GetWorld().EntitiesAt(new Location(col, row)));
            CellListBox.ItemsSource = cellEntities;

            string cellText = "Cell [" + row + " ," + col + "]";

            var board = App.AnthillModel.CurrentWorld.Board;
            
            var location = new Location(col, row);
            var zone = board.Get(location);
                    
            if (zone.Name == "Ground")
            {
                var ground = (Ground) zone;
                cellText += " // Pheromones: " + ground.Pheromones.Count;
            }

            ClickedCellName.Text = cellText;
        }
    }
}
