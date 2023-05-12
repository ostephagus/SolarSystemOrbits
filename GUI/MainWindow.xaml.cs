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

        //public void Execute(object parameter) //Show a new instance of Challenge1
        //{
        //    Challenge1 challengeWindow = new Challenge1();
        //    Window mainWindow = Application.Current.MainWindow;

        //    // Set the position of the Challenge1 window to match the position of the MainWindow
        //    challengeWindow.Left = mainWindow.Left;
        //    challengeWindow.Top = mainWindow.Top;

        //    // Close the MainWindow and show the Challenge1 window
        //    mainWindow.Close();
        //    challengeWindow.Show();
        //}

        public void Execute(object parameter)
        {
            Window challengeHolder = new Window();
            Window mainWindow = Application.Current.MainWindow;

            // Set the position of the Challenge1 window to match the position of the MainWindow
            challengeHolder.Left = mainWindow.Left;
            challengeHolder.Top = mainWindow.Top;

            // Close the MainWindow and show the Challenge1 window
            challengeHolder.Height = mainWindow.Height;
            challengeHolder.Width = mainWindow.Width;
            challengeHolder.Content = new Challenge1UserControl();
            mainWindow.Close();
            challengeHolder.Show();
        }
    }

    public class Command_OpenPlanetSelectionDialog : ICommand
    {
        private Task<Dictionary<SolarSystemBody, bool>> PlanetSelectionTask; //Tasks to allow for asynchronous user input
        private TaskCompletionSource<Dictionary<SolarSystemBody, bool>> PlanetSelectionTaskCompletionSource;
        public static event EventHandler<PlanetSelectionEventArgs> UserHasMadeSelection; //Event to be raised when user preses OK button.

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) //Return true since the command can always execute
        {
            return true;
        }

        public void Execute(object parameter) //Execute method for Interface compliance, opens the dialog but does not get info from it
        {
            PlanetSelectionPopup selectionDialog = new PlanetSelectionPopup((Dictionary<SolarSystemBody, bool>)parameter);
            selectionDialog.Show();
        }

        public async Task<Dictionary<SolarSystemBody, bool>> ExecuteWithReturnAsync(Dictionary<SolarSystemBody, bool> parameter) //Deviation from the standard ICommand to allow for a value to be returned asyncrhonously
        {
            //Create popup
            PlanetSelectionPopup selectionDialog = new PlanetSelectionPopup(parameter);
            selectionDialog.Show();

            //Initialise tasks
            PlanetSelectionTaskCompletionSource = new TaskCompletionSource<Dictionary<SolarSystemBody, bool>>();
            PlanetSelectionTask = PlanetSelectionTaskCompletionSource.Task;

            //Attach an event handler to handle when the user makes the selection
            UserHasMadeSelection += UserHasMadeSelectionEventHandler;

            //Await the user making their selection
            Dictionary<SolarSystemBody, bool> returnDict = await PlanetSelectionTask;
            selectionDialog.Close();

            //Return the final dictionary
            return returnDict;
        }

        private void UserHasMadeSelectionEventHandler(object sender, PlanetSelectionEventArgs e)
        {
            UserHasMadeSelection -= UserHasMadeSelectionEventHandler;

            PlanetSelectionTaskCompletionSource.SetResult(e.SelectedBodies); //Set result completes the task and allows for returning of the value via the Task.
        }

        public static void RaiseUserMadeSelection(object sender, PlanetSelectionEventArgs args) //This method simply is a container for the Event, but allows for the Event to be accessed from other classes without initialising this one again.
        {
            UserHasMadeSelection.Invoke(sender, args);
        }
    }

    public class PlanetSelectionEventArgs : EventArgs //An EventArgs that allows a Dictionary of planets and bools to be sent along with it.
    {
        public Dictionary<SolarSystemBody, bool> SelectedBodies { get; set; }

        public PlanetSelectionEventArgs(Dictionary<SolarSystemBody, bool> selectedBodies)
        {
            SelectedBodies = selectedBodies;
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
