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
        public Command_ChangeWindow Command_ChangeWindow { get; } = new Command_ChangeWindow();

        public BaseChallengeUserControl(string title)
        {
            InitializeComponent();
            DataContext = this;
            TitleTextBox.Text = title;
        }
    }
}
