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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int reactiveFontSize;
        public int ReactiveFontSize { get { return reactiveFontSize; } 
            set {
                reactiveFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(reactiveFontSize)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private const int fontSizeDivisor = 50;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ReactiveFontSize = (int)ActualWidth / fontSizeDivisor;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReactiveFontSize = (int)ActualWidth / fontSizeDivisor;
        }
    }
}
