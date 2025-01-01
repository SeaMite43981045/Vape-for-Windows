using System.Windows;
using System.Windows.Input;

namespace Vape_for_Windows
{
    internal class Main : Window
    {
        private static Boolean _keydown = false;

        public void main()
        {
            this.Dispatcher.Invoke(new Action(() => 
            {
                if (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.RightShift) && !_keydown)
                {
                    new ui.Click().Show();
                    _keydown = true;
                }
            }));
        }
    }
}
