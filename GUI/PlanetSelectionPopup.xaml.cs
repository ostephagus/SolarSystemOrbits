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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    public enum SolarSystemBody
    {
        Sun,
        Mercury,
        Venus,
        Earth,
        Mars,
        Jupiter,
        Saturn,
        Uranus,
        Neptune,
        Pluto
    }

    /// <summary>
    /// Interaction logic for PlanetSelectionPopup.xaml
    /// </summary>
    public partial class PlanetSelectionPopup : Window
    {
        private Dictionary<SolarSystemBody, bool> bodiesDict;

        public PlanetSelectionPopup(Dictionary<SolarSystemBody, bool> bodiesDict)
        {
            this.bodiesDict = bodiesDict;
            InitializeComponent();
            DataContext = this;
            DisplayPlanetOptions();
        }

        public void DisplayPlanetOptions()
        {
            StackPanel planetStackPanel = new StackPanel();
            foreach (SolarSystemBody planet in bodiesDict.Keys)
            {
                CheckBox planetCheckBox = new CheckBox();
                planetCheckBox.Content = planet.ToString();
                planetCheckBox.IsChecked = bodiesDict[planet];
                planetStackPanel.Children.Add(planetCheckBox);
            }
            this.Content = planetStackPanel;
        }


        private void SetVisibilities()
        {
            foreach (SolarSystemBody solarSystemBody in bodiesDict.Keys) //Set all the planet checkboxes that are dict keys to visible
            {
                CheckBox checkBox = (CheckBox)FindName(solarSystemBody.ToString());
                checkBox.Visibility = Visibility.Visible;
            }
        }
    }
}
