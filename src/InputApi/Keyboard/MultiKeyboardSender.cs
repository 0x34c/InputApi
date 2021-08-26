using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputApi.Keyboard
{
    public class MultiKeyboardSender
    {
        private KeyboardInput[] _Inputs;

        /// <summary>
        /// Constructor for MultiKeyboardSender(Class for sendinge multiple KeyboardInputs at once!)
        /// </summary>
        public MultiKeyboardSender(KeyboardInput[] inputs) => this._Inputs = inputs;

        private Imports.INPUT[] Convert(KeyboardInput arg)
        {
            List<Imports.INPUT> inputs = new List<Imports.INPUT>();
            foreach (Keys k in arg.Keys)
            {
                var input = defaultInput();

                input.U.ki.wScan = (ushort)Imports.MapVirtualKey((uint)k, (uint)Imports.MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC);

                inputs.Add(input);
            }

            return inputs.ToArray();
        }

        private Imports.INPUT defaultInput()
        {
            Imports.INPUT ip = default(Imports.INPUT);

            //https://stackoverflow.com/questions/18647053/sendinput-not-equal-to-pressing-key-manually-on-keyboard-in-c

            //mouse = 0, keyboard = 1, hardware = 2

            ip.type = 1;
            ip.U.ki.time = 0;
            ip.U.ki.wVk = 0;
            ip.U.ki.dwExtraInfo = new UIntPtr(0);

            return ip;
        }

        /// <summary>
        /// Sends the input.
        /// </summary>
        public void Send()
        {
            foreach(KeyboardInput input in _Inputs)
            {
                Imports.INPUT[] inputs = Convert(input);

                if (input.keyboardMethod == KeyboardMethod.KeyDown)
                {
                    inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE; return x; }).ToArray();

                    foreach (var input1 in inputs)
                    {
                        Imports.SendInput(1, new Imports.INPUT[] { input1 }, Marshal.SizeOf(typeof(Imports.INPUT)));
                    }
                }
                else if (input.keyboardMethod == KeyboardMethod.KeyUp)
                {
                    //Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)) * inputs.Length);

                    inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE | Imports.KEYEVENTF.KEYUP; return x; }).ToArray();

                    //Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)) * inputs.Length);

                    foreach (var input1 in inputs)
                    {
                        Imports.SendInput(1, new Imports.INPUT[] { input1 }, Marshal.SizeOf(typeof(Imports.INPUT)));
                    }
                }
                else if (input.keyboardMethod == KeyboardMethod.Both)
                {
                    inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE; return x; }).ToArray();

                    foreach (var input1 in inputs)
                    {
                        Imports.SendInput(1, new Imports.INPUT[] { input1 }, Marshal.SizeOf(typeof(Imports.INPUT)));
                    }

                    inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE | Imports.KEYEVENTF.KEYUP; return x; }).ToArray();

                    foreach (var input1 in inputs)
                    {
                        Imports.SendInput(1, new Imports.INPUT[] { input1 }, Marshal.SizeOf(typeof(Imports.INPUT)));
                    }

                }
            }
        }
    }
}
