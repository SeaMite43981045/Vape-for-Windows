using System.Windows;
using Vape_for_Windows.ui;

namespace Vape_for_Windows
{
    internal class Main : Window
    {

        public void main()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                new Click().Topmost = false;
                new Click().Topmost = true;
            }));
        }

        public void onEnable()
        {
        }
    }
}
