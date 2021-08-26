using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputApi.Mouse
{
    public struct MouseInput
    {
        public Button Button;
        public int X;
        public int Y;
    }
    public enum Button
    {
        Left = 1,
        Right = 2,
        Middle = 3,
    }
}
