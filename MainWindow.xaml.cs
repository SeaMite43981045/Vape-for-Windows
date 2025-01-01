using System.Windows;

using Vape_for_Windows.ui;

namespace Vape_for_Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    
    public partial class MainWindow : Window
    {
        private Main _main = new Main();

        public MainWindow()
        {
            InitializeComponent();

            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;

            Button_inject.Text = "Inject";
        }

        private void WindowClose(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void WindowMin(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Inject(object sender, RoutedEventArgs e) 
        {
            this.InjectButton.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;

            MainGui mainGui = new MainGui();
            Click click = new Click();

            mainGui.Show();
            //clickGuiWindow.Show();
            click.Show();

            new NotificationWindow().send("Finish", "Inject successfully", 0, 5);
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            _main.main();
        }
    }
}