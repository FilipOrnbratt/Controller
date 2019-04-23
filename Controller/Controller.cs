using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SlimDX.DirectInput;
using SlimDX;
using System.Threading;
//using SlimDX;
//using SlimDX.DirectInput;

namespace Controller
{
    class Controller
    {
        Joystick joystick;
        private int x = 0, y = 0, rX = 0, rY = 0; //-10 - 10
        private bool connected = false;
        private bool[] buttons = new bool[128];
        private enum RightStick {x, y};
        private enum LeftStick {x, y};
        public struct Stick
        {
            public Stick(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;
        }
       
        public Controller()
        {
            reconnect();
        }

        public void reconnect()
        {
            while (!init())
            {
                Thread.Sleep(500);
            }
            connected = true;
        }

        private bool init()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                return false;
            }

            // Instantiate the joystick
            joystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Console.WriteLine("Effect available {0}", effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();
            return true;
        }

        public void update()
        {
            Result res = joystick.Poll();
            if (res.IsFailure)
                connected = init();
            else
            {
                //var datas = joystick.GetBufferedData();
                JoystickState state = joystick.GetCurrentState();
                bool[] joystick_buttons = state.GetButtons();

                x = (state.X - 65535 / 2) / (65535 / 20);
                y = (state.Y - 65535 / 2) / (65535 / 20);
                rX = (state.RotationX - 65535 / 2) / (65535 / 20);
                rY = (state.RotationY - 65535 / 2) / (65535 / 20);

                if (Math.Abs(x) < 2)
                    x = 0;
                if (Math.Abs(y) < 2)
                    y = 0;
                if (!Enumerable.SequenceEqual(joystick_buttons, buttons))
                {
                    buttons = joystick_buttons;
                    for (int i = 0; i < buttons.Length; i++)
                        if (buttons[i] != joystick_buttons[i])
                            Console.WriteLine(i);
                }
            }
        }

        public bool getButtonStatus(string button)
        {
            int index = -1;
            switch (button)
            {
                case "A":
                    index = 0;
                    break;
                case "B":
                    index = 1;
                    break;
                case "X":
                    index = 2;
                    break;
                case "Y":
                    index = 3;
                    break;
                case "LB":
                    index = 4;
                    break;
                case "RB":
                    index = 5;
                    break;
                case "SELECT":
                    index = 6;
                    break;
                case "START":
                    index = 7;
                    break;
                case "LS":
                    index = 8;
                    break;
                case "RS":
                    index = 9;
                    break;
            }
            return buttons[index];
        }

        public Stick getLeftStick()
        {
            return new Stick(x, y);
        }

        public Stick getRightStick()
        {
            return new Stick(rX, rY);
        }

        public bool isConnected()
        {
            return connected;
        }
    }
}
