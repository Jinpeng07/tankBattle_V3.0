using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class Form1 : Form
    {
        private static Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            windowG = this.CreateGraphics();

            tempBmp = new Bitmap(450,450);
            Graphics bmpG = Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;

            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();

        }

        public static void GameMainThread()
        {
            GameFramework.Start();

            int sleepTime = 1000 / 60;
            while (true)
            {
                GameFramework.g.Clear(Color.Black);
                GameFramework.Update();
                windowG.DrawImage(tempBmp, 0, 0);
                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.keyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.keyUp(e);
        }
    }
}
