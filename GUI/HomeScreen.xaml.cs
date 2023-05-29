using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl, INotifyPropertyChanged
    {
        private int reactiveFontSize;
        public int ReactiveFontSize
        {
            get { return reactiveFontSize; }
            set
            {
                reactiveFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(reactiveFontSize)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand Command_ChangeWindow { get; } = new Command_ChangeWindow();

        private const int fontSizeDivisor = 48;

        public HomeScreen()
        {
            InitializeComponent();
            DataContext = this;
            if (ActualWidth > 0)
            {
                ReactiveFontSize = (int)ActualWidth / fontSizeDivisor;
            }
            else
            {
                ReactiveFontSize = 16;
            }
        }

        private void HomeScreen_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReactiveFontSize = (int)ActualWidth / fontSizeDivisor;
        }
    }
}
