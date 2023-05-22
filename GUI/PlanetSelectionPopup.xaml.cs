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
    /// <summary>
    /// Interaction logic for PlanetSelectionPopup.xaml
    /// </summary>
    public enum SolarSystemBody //Enum for bodies in the solar system, for ease of cross-compatibility and a little bit of memory efficiency
    {
        Sun, //Sun included mainly for task 7
        Mercury,
        Venus,
        Earth,
        Mars,
        Jupiter,
        Saturn,
        Uranus,
        Neptune,
        Pluto //Pluto is a planet :P (no seriously, it's for task 4).
    }

    public partial class PlanetSelectionPopup : Window
    {
        private Dictionary<SolarSystemBody, bool> bodiesDict; //This is to store the status of each planet (used for the checkboxes).

        private List<CheckBox> checkBoxes; //This is to store references to all of the checkboxes, for use later.

        public PlanetSelectionPopup(Dictionary<SolarSystemBody, bool> bodiesDict)
        {
            this.bodiesDict = bodiesDict;
            checkBoxes = new List<CheckBox>();
            InitializeComponent();
            DataContext = this;
            DisplayPlanetOptions();
        }

        public void DisplayPlanetOptions()
        {
            StackPanel planetStackPanel = planetsStackPanel; //Accesses the StackPanel to hold the checkboxes. These have to be loaded in CodeBehind beecause the planets included may differ.
            foreach (SolarSystemBody planet in bodiesDict.Keys)
            {
                CheckBox planetCheckBox = new CheckBox();
                planetCheckBox.Name = planet.ToString();
                planetCheckBox.Content = planet.ToString();
                planetCheckBox.IsChecked = bodiesDict[planet]; //Check the checkboxes according to the dictionary
                planetStackPanel.Children.Add(planetCheckBox); //Finally, add the checkbox to the StackPanel...
                checkBoxes.Add(planetCheckBox); //... and add it to the list for access later.
            }
        }

        public Dictionary<SolarSystemBody, bool> GetSelectedBodies() //Loop through the checkboxes to update the dictionary
        {
            for (int i = 0; i < bodiesDict.Count; i++)
            {
                CheckBox checkBox = checkBoxes[i];
                bodiesDict[bodiesDict.Keys.ElementAt(i)] = (bool)checkBox.IsChecked; //IsChecked will not be null because the checkbox is a dual-state not tri-state
            }

            return bodiesDict; //Return the dict updated with the user's choices. This is not done dynamically because it is too computationally inefficient.
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Command_OpenPlanetSelectionDialog.RaiseUserMadeSelection(this, new PlanetSelectionEventArgs(GetSelectedBodies())); //Raise the event for updating the dictionary on button press.
        }
    }
}
