using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controller
{
    public static class Keyboard
    {
        private static bool inAppWindow = false;
        public static void pressEnter()
        {
            SendKeys.SendWait("{ENTER}");
        }

        public static void pressUp()
        {
            SendKeys.SendWait("{UP}");
        }

        public static void pressLeft()
        {
            SendKeys.SendWait("{LEFT}");
        }

        public static void pressRight()
        {
            SendKeys.SendWait("{RIGHT}");
        }

        public static void pressDown()
        {
            SendKeys.SendWait("{DOWN}");
        }

        public static void pressBackSpace()
        {
            SendKeys.SendWait("{BACKSPACE}");
            
        }

        public static void toggleAppView()
        {
            SendKeys.SendWait("^%{TAB}");
        }
    }
}
