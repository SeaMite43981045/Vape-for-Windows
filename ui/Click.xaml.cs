using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Vape_for_Windows.Modules.SYSTEM;
using Vape_for_Windows.ui.ClickGui;

namespace Vape_for_Windows.ui
{
    /// <summary>
    /// Click.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class Click : Window
    {
        private static Boolean _SystemEnable = false;
        SystemBackground _SystemBackground = new SystemBackground { SystemButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A191A")), SystemControlForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2A2A2")) };

        private SolidColorBrush _enable = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF058569"));
        private SolidColorBrush _disable = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A1A1A1A1"));

        private Boolean _systemFlag = false;
        private Boolean _displayFlag = false;

        private Boolean _module_ShutdownFlag = false;
        private Boolean _module_RebootFlag = false;

        bool _isMouseDown = false;
        Point _mouseDownPosition;
        Thickness _mouseDownMargin;

        public Click()
        {
            InitializeComponent();

            this.Left = 0;
            this.Top = 0;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Loaded += Window_Loaded;
            this.ShowInTaskbar = false;

            //this.ClickGuiMenuGrid.Children.Add(clickMenu);
            this.DataContext = _SystemBackground;
        }

        private void SystemButton_Click(object sender, RoutedEventArgs e)
        {
            if (_systemFlag)
            {
                SystemIcon.Foreground = _disable;
                SystemText.Foreground = _disable;

                ClickGuiSystem.Visibility = Visibility.Hidden;
                this._systemFlag = !_systemFlag;
            }
            else
            {
                SystemIcon.Foreground = _enable;
                SystemText.Foreground = _enable;

                ClickGuiSystem.Visibility = Visibility.Visible;
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

        private void Module_ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            if (_module_ShutdownFlag)
            {
                Module_ShutdownText.Foreground = _disable;
                this._module_ShutdownFlag = !_module_ShutdownFlag;

                new Shutdown().run(ShutdownType.POWEROFF);
            }
            else
            {
                Module_ShutdownText.Foreground = _enable;
                this._module_ShutdownFlag = !_module_ShutdownFlag;
            }
        }

        private void Module_RebootButton_Click(object sender, RoutedEventArgs e)
        {
            if (_module_RebootFlag)
            {
                Module_RebootText.Foreground = _disable;
                this._module_RebootFlag = !_module_RebootFlag;

                new Shutdown().run(ShutdownType.REBOOT);
            }
            else
            {
                Module_RebootText.Foreground = _enable;
                this._module_RebootFlag = !_module_RebootFlag;
            }
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

        // Window Load
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);

            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }
        #region Window styles
        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("114514");
        }
    }
}
