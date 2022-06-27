using System.Windows;
using System.Windows.Input;
using ApplicationForBD.ApplicationDataBases;
using ApplicationForBD.Pages;
using System.Data.SqlClient;

namespace ApplicationForBD
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            SaveElementFrame.frame = FrameMain;
            SaveElementFrame.Window = this;
            SaveElementFrame.frame.Navigate(new LoginPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)=>
            this.Close();
        

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        
    }
}
