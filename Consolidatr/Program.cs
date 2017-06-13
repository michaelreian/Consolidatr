using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consolidatr
{
    public class Program
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int uFlags);

        private const short SWP_NOZORDER = 0X4;
        private const int SWP_SHOWWINDOW = 0x0040;

        public static void Main(string[] args)
        {
            var screens = Screen.AllScreens;

            var primaryScreen = screens.Single(x => x.Primary);

            var processes = Process.GetProcesses(".").Where(x => !string.IsNullOrEmpty(x.MainWindowTitle));
            foreach (var process in processes)
            {
                var handle = process.MainWindowHandle;
                if (handle != IntPtr.Zero)
                {
                    SetWindowPos(handle, 0, primaryScreen.Bounds.Location.X, primaryScreen.Bounds.Location.Y, 640, 480, SWP_NOZORDER | SWP_SHOWWINDOW);
                    process.Refresh();
                }
            }

            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
        }
    }
}
