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
    /// Interaction logic for BaseChallengeUserControl.xaml
    /// </summary>
    public abstract partial class BaseChallengeUserControl : UserControl
    {
        public string Title { get; set; }
        private Command_CloseWindow Command_CloseWindow { get; } = new Command_CloseWindow();

        public BaseChallengeUserControl(string title)
        {
            InitializeComponent();
            DataContext = this;
            TitleTextBox.Text = title;

            closeButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Command_CloseWindow.Execute(Parent); //On click of CloseButton, execute the CloseWindow command with the containing Window as parameter.
        }
    }
}
