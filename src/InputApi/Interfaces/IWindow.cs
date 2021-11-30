using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputApi.Interfaces
{
    public interface IWindow
    {
        Process Process { get; }
        IntPtr Handle { get; }
        string Title { get; }
    }
}
