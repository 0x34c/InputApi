using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using InputApi.Interfaces;

namespace InputApi.Processes
{
    public class Window : IWindow
    {
        public Process Process { get; }
        public IntPtr Handle { get; }
        public string Title { get; }



        private Window(Process prcs, IntPtr handle, string title)
        {
            this.Process = prcs;
            this.Handle = handle;
            this.Title = title;
        }

        public static Window[] GetAllWindows(Process process)
        {
            List<Window> windows = new List<Window>();

            foreach (ProcessThread thread in Process.GetProcessById(process.Id).Threads)
                Imports.EnumThreadWindows(thread.Id, (hwnd, lParam) =>
                {
                    windows.Add(new Window(process, hwnd, Imports.GetWindowTitle(hwnd)));
                    return true;
                }, IntPtr.Zero);

            return windows.ToArray();
        }

        public Window[] GetChildWindows()
        {
            List<Window> windows = new List<Window>();

            Imports.EnumChildWindows(this.Handle, (hwnd, lParam) =>
            {
                windows.Add(new Window(this.Process, hwnd, Imports.GetWindowTitle(hwnd)));
                return true;
            }, IntPtr.Zero);

            return windows.ToArray();
        }

    }
}
