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
        public int ReactiveFontSize { 
            get { return reactiveFontSize; } 
            set {
                reactiveFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(reactiveFontSize)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Command_ChangeToWindow1 Command_ChangeToWindow1 { get; } = new Command_ChangeToWindow1();

        private const int fontSizeDivisor = 50;

        public MainWindow()
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReactiveFontSize = (int)ActualWidth / fontSizeDivisor;
        }
    }

    public class Command_ChangeToWindow1 : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) //Return true since the command can always execute
        {
            return true;
        }

        public void Execute(object parameter) //Show a new instance of Challenge1
        {
            Challenge1 challengeWindow = new Challenge1();
            Window mainWindow = Application.Current.MainWindow;

            // Set the position of the Challenge1 window to match the position of the MainWindow
            challengeWindow.Left = mainWindow.Left;
            challengeWindow.Top = mainWindow.Top;

            // Close the MainWindow and show the Challenge1 window
            mainWindow.Close();
            challengeWindow.Show();
        }
    }

    public class Command_OpenPlanetSelectionDialog : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) //Return true since the command can always execute
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PlanetSelectionPopup selectionDialog = new PlanetSelectionPopup((Dictionary<SolarSystemBody, bool>)parameter);
            selectionDialog.Show();
        }
    }

    public class Command_CloseWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) //Return true since the command can always execute
        {
            return true;
        }

        // Define the logic to execute when the command is executed
        public void Execute(object parameter)
        {
            Window currentWindow = (Window)parameter;
            MainWindow mainWindow = new MainWindow();
            //Set the position of the MainWindow to match the position of the previous Challenge window
            mainWindow.Left = currentWindow.Left;
            mainWindow.Top = currentWindow.Top;

            // Close the current window and show the MainWindow
            currentWindow.Close();
            mainWindow.Show();
        }
    }
}
