using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;

namespace Game
{
    class FormTest
    {
        private Form form = new Form();
        private int x = 200, y = 200;
        private bool left = false, right = false, up = false, down = false;
        private const int FPS = 60, UPS = 100;
        private List<Enemy> list = new List<Enemy>();

        private static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("http://www.gooogle.com");
            Console.WriteLine(downloadString);
            Console.Read();
            //new FormTest();
        }

        public FormTest()
        {
            initForm();

            list.Add(new Enemy(120, 60));
            list.Add(new Enemy(400, 30));
            list.Add(new Enemy(260, 200));

            new Thread(new ThreadStart(update)).Start();

            //Show window
            form.ShowDialog();
        }

        private void initForm()
        {
            form.Text = "Test";
            form.BackColor = Color.White;
            form.Size = new Size(720, 480);

            //Handlers
            form.Paint += Form_Paint;
            form.MouseClick += Form_MouseClick;
            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
            form.FormClosed += Form_FormClosed;

            // Repaint timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (1000 / FPS);
            timer.Tick += new EventHandler(repaint);
            timer.Start();
        }

        private void update()
        {
            while (true)
            {
                Thread.Sleep(1000 / UPS);
                if (left)
                {
                    x--;
                }
                if (right)
                {
                    x++;
                }
                if (up)
                {
                    y--;
                }
                if (down)
                {
                    y++;
                }
            }
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen blackPen = new Pen(Color.Black, 3);
            g.DrawEllipse(blackPen, new Rectangle(x - 5, y - 5, 10, 10));
            foreach (Enemy enemy in list)
            {
                enemy.draw(g);
            }
        }

        private void repaint(object sender, EventArgs e)
        {
            form.Refresh();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                left = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = true;
            }
            else if (e.KeyCode == Keys.W)
            {
                up = true;
            }
            else if (e.KeyCode == Keys.S)
            {
                down = true;
            }
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                left = false;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = false;
            }
            else if (e.KeyCode == Keys.W)
            {
                up = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                down = false;
            }
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }
    }
}
