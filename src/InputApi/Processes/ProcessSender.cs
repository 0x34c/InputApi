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
        private MouseInput? _msInput = null;
        private IWindow _window;

        public ProcessSender(KeyboardInput input, IWindow window)
        {
            _window = window;
            _kbInput = input;
        }
        public ProcessSender(MouseInput msInput, IWindow window)
        {
            _window = window;
            _msInput = msInput;
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
                switch(_msInput?.Button)
                {
                    case Button.Left:
                        Imports.PostMessage(_window.Handle, Imports.WM_LBUTTONDOWN, IntPtr.Zero, new IntPtr(Imports.MakeLParam(_msInput.Value.X, _msInput.Value.Y)));
                        Imports.PostMessage(_window.Handle, Imports.WM_LBUTTONUP, IntPtr.Zero, new IntPtr(Imports.MakeLParam(_msInput.Value.X, _msInput.Value.Y)));
                        break;
                    case Button.Right:
                        Imports.PostMessage(_window.Handle, Imports.WM_RBUTTONDOWN, IntPtr.Zero, new IntPtr(Imports.MakeLParam(_msInput.Value.X, _msInput.Value.Y)));
                        Imports.PostMessage(_window.Handle, Imports.WM_RBUTTONUP, IntPtr.Zero, new IntPtr(Imports.MakeLParam(_msInput.Value.X, _msInput.Value.Y)));
                        break;
                    case Button.Middle:
                        Imports.PostMessage(_window.Handle, Imports.WM_MBUTTONDOWN, IntPtr.Zero, new IntPtr(Imports.MakeLParam(_msInput.Value.X, _msInput.Value.Y)));
                        Imports.PostMessage(_window.Handle, Imports.WM_MBUTTONUP, IntPtr.Zero, new IntPtr(Imports.MakeLParam(_msInput.Value.X, _msInput.Value.Y)));
                        break;
                }
            }
        }
    }
}
