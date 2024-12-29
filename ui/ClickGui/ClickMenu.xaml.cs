using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace Vape_for_Windows.ui.ClickGui
{
    /// <summary>
    /// Menu.xaml 的交互逻辑
    /// </summary>
    public partial class ClickMenu : UserControl
    {
        private static Boolean _SystemEnable = false;

        SystemBackground _SystemBackground = new SystemBackground { SystemButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A191A")), SystemControlForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2A2A2")) };

        public ClickMenu()
        {
            InitializeComponent();
            new NotificationWindow().send("Clicked", "Clicked", 0, 3);

            this.DataContext = _SystemBackground;
        }

        private void SystemButton_Click(object sender, RoutedEventArgs e)
        {
            _SystemBackground.SystemButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1F1E1F"));
            new NotificationWindow().send("Clicked", "Clicked", 0, 3);
        }

        public class SystemBackground : INotifyPropertyChanged
        {
            private SolidColorBrush systemButtonBackgroundValue;
            private SolidColorBrush systemControlForegroundValue;

            public event PropertyChangedEventHandler? PropertyChanged;

            public SolidColorBrush SystemButtonBackground
            {
                get { return systemButtonBackgroundValue; }
                set
                {
                    if (systemButtonBackgroundValue != value)
                    {
                        systemButtonBackgroundValue = value;
                        OnPropertyChanged();
                    }
                }
            }

            public SolidColorBrush SystemControlForeground
            {
                get { return systemControlForegroundValue; }
                set 
                {
                    if (systemControlForegroundValue != value)
                    {
                        systemControlForegroundValue = value;
                        OnPropertyChanged();
                    }
                }
            }

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test button clicked");
        }
    }
}
