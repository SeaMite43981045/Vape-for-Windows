using System.ComponentModel;
using System.Diagnostics;
using Vape_for_Windows.Common.Module;
using Vape_for_Windows.ui;

namespace Vape_for_Windows.Modules.MYTHWARE
{
    internal class KillMythware : Module
    {
        private Boolean _killed = false;

        public KillMythware() : base("KillMythware", Category.MYTHWARE)
        {
        }

        public override void OnEnable()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains("StudentMain"))
                {
                    try
                    {
                        p.Kill();
                        p.WaitForExit();
                        _killed = true;
                        new NotificationWindow().send("Success", "Module KillMythware excuted successfully!", 0, 5);
                    }
                    catch (Win32Exception e)
                    {
                        new NotificationWindow().send("Failed", e.Message.ToString(), 0, 5);
                    }
                    catch (InvalidOperationException e)
                    {
                        new NotificationWindow().send("Failed", e.Message.ToString(), 0, 5);
                    }
                }
            }
            if (!_killed)
            {
                new NotificationWindow().send("Failed", "Process StudentMain.exe no found! ", 0, 5);
            }
        }
    }
}
