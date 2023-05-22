using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;

namespace GUI
{
    class Challenge1UserControl : BaseChallengeUserControl //Since XAML cannot be inherited, these UserControls cannot have XAML components. As a result, all the Controls must be defined in c#, which is a pain
    {

        private Dictionary<SolarSystemBody, bool> solarSystemBodies = new Dictionary<SolarSystemBody, bool>();
        private Command_OpenPlanetSelectionDialog Command_OpenPlanetSelectionDialog { get; } = new Command_OpenPlanetSelectionDialog();

        public Challenge1UserControl() : base("Graph to verify Keppler's 3rd Law") //Set title via constructor
        {
            SetUpGraph();
            AddButtons();
            InitialiseDictionary();
        }

        public void SetUpGraph()
        {
            Challenge1PlotViewModel viewModel = new Challenge1PlotViewModel();
            CartesianChart graph = new CartesianChart();
            graph.Series = viewModel.Series;
            chartHolder.Content = graph;
        }

        public void AddButtons() //The lack of XAML makes this section really quite lengthy
        {
            Button planetsSelectionButton = new Button();
            planetsSelectionButton.Content = "Select planets";
            planetsSelectionButton.Click += PlanetsSelectButton_Click;
            optionsStackPanel.Children.Add(planetsSelectionButton);

            Button planetsResetButton = new Button();
            planetsResetButton.Content = "Reset selected planets";
            planetsResetButton.Click += PlanetsResetButton_Click;
            optionsStackPanel.Children.Add(planetsResetButton);

            CheckBox lineOfBestFitCheckBox = new CheckBox();
            lineOfBestFitCheckBox.Name = "lineOfBestFitCheckBox";
            lineOfBestFitCheckBox.IsChecked = true;
            lineOfBestFitCheckBox.Content = "Line of best fit";
            optionsStackPanel.Children.Add(lineOfBestFitCheckBox);
        }

        public void InitialiseDictionary()
        {
            solarSystemBodies.Clear(); //For reset button
            for (int i = 1; i <= 8; i++)
            {
                solarSystemBodies.Add((SolarSystemBody)i, true); //Set all the planets and select all
            }
        }

        private void PlanetsSelectButton_Click(object sender, RoutedEventArgs e)
        {
            MakePlanetSelectionAsync(); //Asynchronously wait for the user to make their planet selection. Handled by the below Async method
        }

        private async void MakePlanetSelectionAsync()
        {
            solarSystemBodies = await Command_OpenPlanetSelectionDialog.ExecuteWithReturnAsync(solarSystemBodies); //Run the Async ICommmand.
        }

        private void PlanetsResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitialiseDictionary(); //Re-run the InitialiseDictionary method to reset all the values to true, selecting them all
        }
    }
}
