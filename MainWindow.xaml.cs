using System.Windows;

using Vape_for_Windows.ui;
using Vape_for_Windows.Common.Utils;
using System;
using System.Windows.Input;

namespace Vape_for_Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 


    public partial class MainWindow : Window
    {
        private Main _main = new Main();
        private KeyboardHook _keyboardHook = new KeyboardHook();

        private Boolean _rightAltPressed = false;
        private Boolean _rightShiftPressed = false;
        private Boolean _rightCtrlPressed = false;

        public MainWindow()
        {
            InitializeComponent();

            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;

            Button_inject.Text = "Inject";
        }

        private void WindowClose(object sender, RoutedEventArgs e)
        {
            this.OnClosed(e);
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

            mainGui.Show();
            //clickGuiWindow.Show();
            Click.showWindow();

            _keyboardHook = new KeyboardHook();
            _keyboardHook.SetHook();
            _keyboardHook.SetOnKeyDownEvent(Win32_Keydown);
            _keyboardHook.SetOnKeyUpEvent(Win32_KeyUp);


            new NotificationWindow().send("Finish", "Press RShift + RAlt + RCtrl to open the ClickGui", 0, 5);

            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            _main.main();
        }

        private void Win32_Keydown(Key key)
        {
            switch (key)
            {
                case Key.RightShift:
                    {
                        _rightShiftPressed = true;
                    }
                    break;
                case Key.RightAlt:
                    {
                        _rightAltPressed = true;
                    }
                    break;
                case Key.RightCtrl:
                    {
                        _rightCtrlPressed = true;
                    }
                    break;
                case Key.Escape:
                    {
                        new Click().closeWindow();
                    }
                    break;
            }

            if (_rightAltPressed && _rightShiftPressed && _rightCtrlPressed)
            {
                Click.showWindow();
            }
        }
        private void Win32_KeyUp(Key key)
        {
            switch (key)
            {
                case Key.RightShift:
                    {
                        _rightShiftPressed = false;
                    }
                    break;
                case Key.RightAlt:
                    {
                        _rightAltPressed = false;
                    }
                    break;
                case Key.RightCtrl:
                    {
                        _rightCtrlPressed = false;
                    }
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {

            _keyboardHook.UnHook();

            base.OnClosed(e);
        }
    }
}