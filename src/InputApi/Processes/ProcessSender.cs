using System;
using InputApi.Keyboard;
using InputApi.Mouse;
using InputApi.Interfaces;
using System.Windows.Forms;
using Button = InputApi.Mouse.Button;
using System.Runtime.InteropServices;

namespace InputApi.Processes
{
    public class ProcessSender : ISender
    {
        private KeyboardInput? _kbInput = null;
        private Button? _msInput = null;
        private Window _window;

        public ProcessSender(KeyboardInput input, Window window)
        {
            _window = window;
            _kbInput = input;
        }
        public ProcessSender(Button button, Window window)
        {
            _window = window;
            _msInput = button;
        }

        
        public void Send()
        {
            if(_kbInput.HasValue)
            {
                switch(_kbInput?.keyboardMethod)
                {
                    case KeyboardMethod.KeyUp:
                        foreach(Keys k in _kbInput?.Keys)
                            Imports.PostMessage(_window.Handle, Imports.WM_KEYUP, (IntPtr)k, IntPtr.Zero);
                        break;
                    case KeyboardMethod.KeyDown:
                        foreach (Keys k in _kbInput?.Keys)
                            Imports.PostMessage(_window.Handle, Imports.WM_KEYDOWN, (IntPtr)k, IntPtr.Zero);
                        break;
                    case KeyboardMethod.Both:
                        foreach (Keys k in _kbInput?.Keys)
                        {
                            Imports.PostMessage(_window.Handle, Imports.WM_KEYUP, (IntPtr)k, IntPtr.Zero);
                            Imports.PostMessage(_window.Handle, Imports.WM_KEYDOWN, (IntPtr)k, IntPtr.Zero);
                        }
                        break;
                }

            }
            else if(_msInput.HasValue)
            {
                switch(_msInput.Value)
                {
                    case Button.Left:
                        break;
                    case Button.Right:
                        break;
                    case Button.Middle:
                        break;
                }
            }
        }
    }
}
