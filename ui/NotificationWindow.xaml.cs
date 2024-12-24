using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vape_for_Windows.ui
{
    /// <summary>
    /// Notification.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();

        public double TopFrom
        {
            get; set;
        }

        public NotificationWindow()
        {
            InitializeComponent();
            this.Loaded += Notification_Loaded;
            this.Loaded += Window_Loaded;
        }

        private void Notification_Loaded(object sender, RoutedEventArgs e)
        {
            NotifyData? data = this.DataContext as NotifyData;
            if (data != null)
            {
                NotificationTitle.Text = data.Title;
                NotificationContent.Text = data.Content;
            }
            NotificationWindow? self = sender as NotificationWindow;
            if (self != null)
            { 
                self.UpdateLayout();

                double right = SystemParameters.WorkArea.Right;
                self.Top = self.TopFrom - self.ActualHeight;
                DoubleAnimation animation = new DoubleAnimation();

                animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                animation.From = right;
                animation.To = right - self.ActualWidth;
                animation.EasingFunction = new CubicEase()
                {
                    EasingMode = EasingMode.EaseOut
                };
                self.BeginAnimation(Window.LeftProperty, animation);

                Task.Factory.StartNew(delegate
                {
                        Thread.Sleep(TimeSpan.FromSeconds(2));

                    this.Dispatcher.Invoke(delegate
                    {
                        animation = new DoubleAnimation();
                        animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                        animation.Completed += (s, a) => { self.Close(); };
                        animation.From = right - self.ActualWidth;
                        animation.To = right;
                        animation.EasingFunction = new CubicEase()
                        {
                            EasingMode = EasingMode.EaseOut
                        };
                        self.BeginAnimation(Window.LeftProperty, animation);
                    });
                });
            }
        }

        public void send(string title, string content, int type)
        {
            NotifyData data = new NotifyData();
            data.Title = title;
            data.Content = content;

            NotificationWindow dialog = new NotificationWindow();
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            _dialogs.Add(dialog);
            dialog.DataContext = data;
            dialog.Show();
        }


        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }
        double GetTopFrom()
        {
            double topFrom = SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 110;
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }

        // Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
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
    }
}
