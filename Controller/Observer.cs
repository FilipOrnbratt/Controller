using System;
using System.Threading;

namespace Controller
{
    class Observer
    {
        private Controller cont;
        private bool leftPressed = false, rightPressed = false, movedLeft = false, movedUp = false, inAppView = false;
        private int counterX = 0, counterY = 0;
        private SystemTray tray;

        static void Main(string[] args)
        {
            new Observer();
        }

        public Observer()
        {
            new Thread(initTray).Start();
            cont = new Controller();
            tray.setConnectedStatus(true);
            update();
        }

        private void initTray()
        {
            tray = new SystemTray();
            tray.start();
        }

        private void update()
        {
            
            while (true)
            {
                Thread.Sleep(10);
                cont.update();
                if (cont.isConnected())
                {
                    Mouse.move(cont.getLeftStick().x, cont.getLeftStick().y);
                    checkButtons();
                    checkLeftStick();
                    checkRightStick();
                }
                else
                {
                    tray.setConnectedStatus(false);
                    cont.reconnect();
                    tray.setConnectedStatus(true);
                }
            }
        }

        private void checkButtons()
        {
            if (cont.getButtonStatus("A") && !leftPressed)
            {
                Mouse.leftDown();
                leftPressed = true;
            }
            else if (!cont.getButtonStatus("A") && leftPressed)
            {
                Mouse.leftUp();
                leftPressed = false;
            }
            if (cont.getButtonStatus("X") && !rightPressed)
            {
                Mouse.rightDown();
                rightPressed = true;
            }
            else if (!cont.getButtonStatus("X") && rightPressed)
            {
                Mouse.rightUp();
                rightPressed = false;
            }
            if (cont.getButtonStatus("Y") && !inAppView)
            {
                Keyboard.toggleAppView();
                inAppView = true;
            }
            else if (!cont.getButtonStatus("Y") && inAppView)
            {
                inAppView = false;
            }
        }

        private void checkLeftStick()
        {
            if ((movedLeft && cont.getRightStick().x > 2) || (!movedLeft && cont.getRightStick().x < -2))
            {
                counterX = 0;
                movedLeft = !movedLeft;
            }
        }

        private void checkRightStick()
        {
            if ((movedUp && cont.getRightStick().y > 2) || (!movedUp && cont.getRightStick().y < -2))
            {
                counterY = 0;
                movedUp = !movedUp;
            }
            if (Math.Abs(cont.getRightStick().x) > 2 && counterX % (10 - Math.Abs(cont.getRightStick().x) + 2) == 0)
            {
                if (cont.getRightStick().x > 0)
                {
                    Keyboard.pressRight();
                }
                else
                {
                    Keyboard.pressLeft();
                }
            }
            if (Math.Abs(cont.getRightStick().y) > 2 && counterY % (10 - Math.Abs(cont.getRightStick().y) + 2) == 0)
            {
                if (cont.getRightStick().y > 0)
                {
                    Keyboard.pressDown();
                }
                else
                {
                    Keyboard.pressUp();
                }
            }
            counterX++;
            counterY++;
        }
    }
}