using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vape_for_Windows.ui.ClickGui;

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
                    new ClickMenu().Show();
                    _keydown = true;
                }
            }));
        }
    }
}
