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
        public Command_CloseWindow Command_CloseWindow { get; } = new Command_CloseWindow();

        public Challenge1()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Command_CloseWindow.Execute(this); //On click of CloseButton, execute the CloseWindow command with the current window as parameter.
        }
    }
}
