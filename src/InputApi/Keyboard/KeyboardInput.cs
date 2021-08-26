using System;
using System.Windows.Forms;

namespace InputApi.Keyboard
{
    public enum KeyboardMethod
    {
        KeyUp = 1,
        KeyDown = 2,
        Both = 3,
    }
    public struct KeyboardInput
    {
        public Keys[] Keys;
        public KeyboardMethod keyboardMethod;
    }
}
