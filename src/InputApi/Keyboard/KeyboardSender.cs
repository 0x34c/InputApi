using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using InputApi.Interfaces;

namespace InputApi.Keyboard
{
    public class KeyboardSender : ISender
    {
        private Keys[] _Keys;
        private KeyboardMethod KeyboardMethod;

        /// <summary>
        /// Constructor for sending keyboard input.
        /// </summary>
        public KeyboardSender(KeyboardInput input)
        {
            this._Keys = input.Keys;
            this.KeyboardMethod = input.keyboardMethod;
        }
        /// <summary>
        /// Constructor for sending keyboard input without KeyboardInput struct!
        /// </summary>
        public KeyboardSender(Keys[] Keys, KeyboardMethod keyboardMethod)
        {
            _Keys = Keys;
            KeyboardMethod = keyboardMethod;
        }
        /// <summary>
        /// Constructor for sending keyboard input without KeyboardInput struct!
        /// </summary>
        public KeyboardSender(Keys key, KeyboardMethod keyboardMethod)
        {
            _Keys = new Keys[] { key };
            KeyboardMethod = keyboardMethod;
        }

        private Imports.INPUT[] Convert(Keys[] keys)
        {
            List<Imports.INPUT> inputs = new List<Imports.INPUT>();
            foreach (Keys k in keys)
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
            Imports.INPUT[] inputs = Convert(_Keys);

            if (KeyboardMethod == KeyboardMethod.KeyDown)
            {
                inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE; return x; }).ToArray();

                foreach (var input in inputs)
                {
                    Imports.SendInput(1, new Imports.INPUT[] { input }, Marshal.SizeOf(typeof(Imports.INPUT)));
                }
            }
            else if (KeyboardMethod == KeyboardMethod.KeyUp)
            {
                //Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)) * inputs.Length);

                inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE | Imports.KEYEVENTF.KEYUP; return x; }).ToArray();

                //Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)) * inputs.Length);

                foreach (var input in inputs)
                {
                    Imports.SendInput(1, new Imports.INPUT[] { input }, Marshal.SizeOf(typeof(Imports.INPUT)));
                }
            }
            else if(KeyboardMethod == KeyboardMethod.Both)
            {
                inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE; return x; }).ToArray();

                foreach (var input in inputs)
                {
                    Imports.SendInput(1, new Imports.INPUT[] { input }, Marshal.SizeOf(typeof(Imports.INPUT)));
                }

                inputs = inputs.Select(x => { x.U.ki.dwFlags = Imports.KEYEVENTF.SCANCODE | Imports.KEYEVENTF.KEYUP; return x; }).ToArray();

                foreach (var input in inputs)
                {
                    Imports.SendInput(1, new Imports.INPUT[] { input }, Marshal.SizeOf(typeof(Imports.INPUT)));
                }

            }
        }
    }
}
