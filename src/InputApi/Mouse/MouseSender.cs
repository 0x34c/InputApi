using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InputApi.Mouse
{
    public class MouseSender
    {
        private Button _Button;
        private int _x;
        private int _y;
        //https://stackoverflow.com/questions/8021954/sendinput-doesnt-perform-click-mouse-button-unless-i-move-cursor


        /// <summary>
        /// Constructor for sending mouse input.
        /// </summary>
        public MouseSender(MouseInput input)
        {
            _Button = input.Button;
            _x = input.X;
            _y = input.Y;
        }
        /// <summary>
        /// Constructor for sending mouse input without MouseInput struct!
        /// </summary>
        public MouseSender(int x, int y, Button button)
        {
            _Button = button;
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Sends the input.
        /// </summary>
        public void Send()
        {
            switch (_Button)
            {
                case Button.Left:
                    ClickLeftButton();
                    break;
                case Button.Right:
                    ClickRightButton();
                    break;
                case Button.Middle:
                    ClickMiddleButton();
                    break;
            }
        }


        private void ClickLeftButton()
        {
            Imports.INPUT[] inputs = new Imports.INPUT[3] { createInput(), createInput(), createInput() };

            inputs[0].U.mi.dwFlags = Imports.MOUSEEVENTF.ABSOLUTE | Imports.MOUSEEVENTF.MOVE;

            inputs[1].U.mi.dwFlags = Imports.MOUSEEVENTF.LEFTDOWN;

            inputs[2].U.mi.dwFlags = Imports.MOUSEEVENTF.LEFTUP;

            Imports.SendInput(3, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));

        }
        private void ClickRightButton()
        {
            Imports.INPUT[] inputs = new Imports.INPUT[3] { createInput(), createInput(), createInput() };

            inputs[0].U.mi.dwFlags = Imports.MOUSEEVENTF.ABSOLUTE | Imports.MOUSEEVENTF.MOVE;

            inputs[1].U.mi.dwFlags = Imports.MOUSEEVENTF.RIGHTDOWN;

            inputs[2].U.mi.dwFlags = Imports.MOUSEEVENTF.RIGHTUP;

            Imports.SendInput(3, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));

        }
        private void ClickMiddleButton()
        {
            Imports.INPUT[] inputs = new Imports.INPUT[3] { createInput(), createInput(), createInput() };

            inputs[0].U.mi.dwFlags = Imports.MOUSEEVENTF.ABSOLUTE | Imports.MOUSEEVENTF.MOVE;

            inputs[1].U.mi.dwFlags = Imports.MOUSEEVENTF.MIDDLEDOWN;

            inputs[2].U.mi.dwFlags = Imports.MOUSEEVENTF.MIDDLEUP;

            Imports.SendInput(3, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));

        }
        private Imports.INPUT createInput()
        {
            Imports.INPUT ip = default(Imports.INPUT);

            //mouse = 0, keyboard = 1, hardware = 2

            ip.type = 0;
            ip.U.mi.dx = (_x * 65536) / Imports.GetSystemMetrics(Imports.SystemMetric.SM_CXSCREEN);
            ip.U.mi.dy = (_y * 65536) / Imports.GetSystemMetrics(Imports.SystemMetric.SM_CYSCREEN);
            ip.U.mi.mouseData = 0;

            return ip;
        }
    }
}
