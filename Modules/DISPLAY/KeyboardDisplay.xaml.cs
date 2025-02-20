using System.Windows;

namespace Vape_for_Windows.Modules.DISPLAY
{
    /// <summary>
    /// KeyboardDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class KeyboardDisplay : Window
    {
        public KeyboardDisplay()
        {
            InitializeComponent();
        }

        public void window_close()
        {
            this.Visibility = Visibility.Hidden;
        }

        public void window_show()
        {
            this.Visibility = Visibility.Visible;
        }
    }
}
