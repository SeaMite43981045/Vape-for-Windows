using System.Windows;
using System.Windows.Media;

namespace Vape_for_Windows.ui.ClickGui
{
    /// <summary>
    /// ClickGuiMenu.xaml 的交互逻辑
    /// </summary>
    public partial class ClickGuiMenu : Window
    {
        private SolidColorBrush _enable = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF058569"));
        private SolidColorBrush _disable = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A1A1A1A1"));

        private Boolean _systemFlag = false;
        private Boolean _displayFlag = false;

        public ClickGuiMenu()
        {
            InitializeComponent();
        }

        private void SystemButton_Click(object sender, RoutedEventArgs e)
        {
            if (_systemFlag)
            {
                SystemIcon.Foreground = _disable;
                SystemText.Foreground = _disable;
                this._systemFlag = !_systemFlag;
            }
            else
            {
                SystemIcon.Foreground = _enable;
                SystemText.Foreground = _enable;
                this._systemFlag = !_systemFlag;
            }
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_displayFlag)
            {
                DisplayIcon.Foreground = _disable;
                DisplayText.Foreground = _disable;
                this._displayFlag = !_displayFlag;
            }
            else
            {
                DisplayIcon.Foreground = _enable;
                DisplayText.Foreground = _enable;
                this._displayFlag = !_displayFlag;
            }
        }
    }
}
