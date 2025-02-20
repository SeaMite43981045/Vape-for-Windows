using System.Diagnostics;
using Vape_for_Windows.Common.Module;

namespace Vape_for_Windows.Modules.MYTHWARE
{
    internal class KillMythware : Module
    {
        public KillMythware() : base("KillMythware", Category.MYTHWARE)
        {
        }

        public override void OnEnable()
        {
            Process[] process = Process.GetProcessesByName("StudentMain.exe");
            foreach (Process p in process)
            {
                Debug.WriteLine(p.ExitCode);
                p.Kill();
            }
        }
    }
}
