using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Vape_for_Windows.Modules.DISPLAY;
using Vape_for_Windows.Modules.SYSTEM;
using Vape_for_Windows.ui.ClickGui;
using Vape_for_Windows.Modules.MYTHWARE;
using System.Windows.Threading;

namespace Vape_for_Windows.ui
{
    /// <summary>
    /// Click.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class Click : Window
    {
        private static Boolean shown = false;

        private nint _handle;

        public SolidColorBrush _enable = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF058569"));
        public SolidColorBrush _disable = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A1A1A1A1"));

        // ----- Flags ----- //

        // module menu
        private Boolean _systemFlag = false;
        private Boolean _displayFlag = false;
        private Boolean _mythwareFlag = false;

        //module button
        public Boolean _module_SuspendMythwareFlag = false;
        private Boolean _module_KeyboardDisplay = false;

        private KeyboardDisplay _keyboardDisplay = new KeyboardDisplay();
        
        private DispatcherTimer _timer;



        private const int HWND_TOPMOST = -1;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);


        public Click()
        {
            InitializeComponent();

            this.Left = 0;
            this.Top = 0;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Loaded += Window_Loaded;
            this.Loaded += Window_Loaded_topmost;
            this.Loaded += Window_Loaded_topmost_win32;
            this.ShowInTaskbar = false;
            this.Closing += OnClosed;

            //this.ClickGuiMenuGrid.Children.Add(clickMenu);
        }

        public static void showWindow()
        {
            if (!shown)
            {
                shown = true;
                new Click().Topmost = true;
                new Click().Show();
            }
        }
        public void closeWindow()
        {
            if (shown)
            {
                this.Visibility = Visibility.Hidden;
                Application.Current.Shutdown();
            }
        }

        protected void OnClosed(object sender, CancelEventArgs e)
        {
            shown = false;
        }

        // Main menu button click event
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

                ClickGuiDisplay.Visibility = Visibility.Hidden;
                this._displayFlag = !_displayFlag;

            }
            else
            {
                DisplayIcon.Foreground = _enable;
                DisplayText.Foreground = _enable;

                ClickGuiDisplay.Visibility = Visibility.Visible;
                this._displayFlag = !_displayFlag;

            }
        }
        private void MythwareButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mythwareFlag)
            {
                MythwareIcon.Foreground = _disable;
                MythwareText.Foreground = _disable;

                ClickGuiMythware.Visibility = Visibility.Hidden;
                this._mythwareFlag = !_mythwareFlag;

            }
            else
            {
                MythwareIcon.Foreground = _enable;
                MythwareText.Foreground = _enable;

                ClickGuiMythware.Visibility = Visibility.Visible;
                this._mythwareFlag = !_mythwareFlag;

            }
        }

        private void Window_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Button Click Event
        private void Module_LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            new Shutdown().run(ShutdownType.LOGOFF);
        }
        private void Module_ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            new Shutdown().run(ShutdownType.POWEROFF);
        }

        private void Module_RebootButton_Click(object sender, RoutedEventArgs e)
        {
            new Shutdown().run(ShutdownType.REBOOT);
        }

        private void Module_NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            new NotificationWindow().send("test", "test", 0, 3);
        }

        private void Module_KillMythware_Click(object sender, RoutedEventArgs e)
        {
            new KillMythware().OnEnable();
        }

        private void Module_SuspendMythware_Click(object sender, RoutedEventArgs e)
        {
            if (_module_SuspendMythwareFlag)
            {
                Module_SuspendMythwareText.Foreground = _disable;

                new SuspendMythware().OnDisable();

                this._module_SuspendMythwareFlag = !_module_SuspendMythwareFlag;

            }
            else
            {
                Module_SuspendMythwareText.Foreground = _enable;
                this._module_SuspendMythwareFlag = !_module_SuspendMythwareFlag;

                new SuspendMythware().OnEnable();

            }
        }

        private void Module_KeyboardDisplay_Click(object sender, RoutedEventArgs e)
        {
            if (_module_KeyboardDisplay)
            {
                this.Module_KeyboardDisplayText.Foreground = _disable;

                _keyboardDisplay.window_close();

                this._module_KeyboardDisplay = !this._module_KeyboardDisplay;
            }
            else
            {
                this.Module_KeyboardDisplayText.Foreground = _enable;

                _keyboardDisplay.Show();
                _keyboardDisplay.window_show();

                this._module_KeyboardDisplay = !this._module_KeyboardDisplay;
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

        private void Window_Loaded_topmost(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }
        private void Window_Loaded_topmost_win32(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }
    }
}
