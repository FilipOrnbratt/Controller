using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace Controller
{
    class SystemTray
    {
        private NotifyIcon tray;
        private ContextMenu menu;
        private MenuItem statusItem;
        public SystemTray()
        {
            CreateIcon();
        }

        public void start()
        {
            Application.Run();
        }

        public void setConnectedStatus(bool status)
        {
            Console.WriteLine(status);
            if (status)
            {
                tray.Icon = new Icon("iconC.ico");
                statusItem.Text = "Connected";
            }
            else
            {
                tray.Icon = new Icon("iconR.ico");
                statusItem.Text = "Reconnecting...";
            }
        }

        private void CreateIcon()
        {
            IContainer components = new Container();
            menu = new ContextMenu();

            statusItem = new MenuItem();
            statusItem.Index = 0;
            statusItem.Text = "Connecting...";

            MenuItem exitItem = new MenuItem();
            exitItem.Index = 1;
            exitItem.Text = "Exit";
            exitItem.Click += new System.EventHandler(this.exit_Click);

            menu.MenuItems.AddRange(new MenuItem[] { statusItem, exitItem });

            tray = new NotifyIcon(components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            tray.Icon = new Icon("iconD.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            tray.ContextMenu = this.menu;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            tray.Text = "XBOX Controller app";
            tray.Visible = true;

            // Handle the DoubleClick event to activate the form.
            tray.DoubleClick += new EventHandler(this.notifyIcon1_DoubleClick);
            tray.Click += new EventHandler(this.notifyIcon1_Click);
        }
        private void notifyIcon1_Click(object Sender, EventArgs e)
        {
            //MessageBox.Show("clicked");
        }

        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            //MessageBox.Show("Double clicked");
        }

        private void exit_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            tray.Visible = false;
            Environment.Exit(0);
        }

    }
}