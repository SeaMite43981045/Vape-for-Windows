using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vape_for_Windows.Common.Module;
using Vape_for_Windows.ui;

namespace Vape_for_Windows.Modules.MYTHWARE
{

    internal class SuspendMythware : Module
    {
        private int _id;
        public SuspendMythware() : base("SuspendMythware", Category.MYTHWARE)
        {
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSuspendProcess(IntPtr processHandle);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtResumeProcess(IntPtr processHandle);

        public static void SuspendProcess(int processId)
        {
            Process process = Process.GetProcessById(processId);
            IntPtr handle = process.Handle;
            NtSuspendProcess(handle);
        }
        public static void ResumeProcess(int processId)
        {
            Process process = Process.GetProcessById(processId);
            IntPtr handle = process.Handle;
            NtResumeProcess(handle);
        }

        public override void OnEnable()
        {
            if (Process.GetProcessesByName("StudentMain").Length != 0)
            {
                _id = Process.GetProcessesByName("StudentMain").First().Id;
                try
                {
                    SuspendProcess(_id);
                }
                catch (Win32Exception e)
                {
                    new NotificationWindow().send("Failed", e.Message.ToString(), 0, 5);
                }
                catch (Exception e)
                {
                    new NotificationWindow().send("Failed", e.Message.ToString(), 0, 5);
                }
            }
            else
            {
                Click click = new Click();

                new NotificationWindow().send("Failed", "Process \"StudentMain\" no found!", 0, 5);
                click.Module_SuspendMythwareText.Foreground = click._disable;
                click._module_SuspendMythwareFlag = true;
            }
        }
        public override void OnDisable()
        {

            try
            {
                ResumeProcess(_id);
            }
            catch (Win32Exception e)
            {
                new NotificationWindow().send("Failed", e.Message.ToString(), 0, 5);
            }
            catch (Exception e)
            {
                new NotificationWindow().send("Failed", e.Message.ToString(), 0, 5);
            }
        }
    }
}
