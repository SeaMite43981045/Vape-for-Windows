using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

using Vape_for_Windows.ui;
using Vape_for_Windows.ui.ClickGui;

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
            this.Close();
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
            ClickMenu clickMenu = new ClickMenu();

            mainGui.Show();
            clickMenu.Show();

            new NotificationWindow().send("Finish", "Inject successfully", 0, 5);
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            _main.main();
        }
    }
}