using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Challenge1.xaml
    /// </summary>
    public partial class Challenge1 : Window
    {
        private Dictionary<SolarSystemBody, bool> solarSystemBodies = new Dictionary<SolarSystemBody, bool>();
        public Command_CloseWindow Command_CloseWindow { get; } = new Command_CloseWindow();
        public Command_OpenPlanetSelectionDialog Command_OpenPlanetSelectionDialog { get; } = new Command_OpenPlanetSelectionDialog();

        public Challenge1()
        {
            InitializeComponent();
            DataContext = this;
            InitialiseDictionary();
        }

        public void InitialiseDictionary()
        {
            solarSystemBodies.Clear(); //For reset button
            for (int i = 1; i <= 8; i++)
            {
                solarSystemBodies.Add((SolarSystemBody)i, true); //Set all the planets and select all
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Command_CloseWindow.Execute(this); //On click of CloseButton, execute the CloseWindow command with the current window as parameter.
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
