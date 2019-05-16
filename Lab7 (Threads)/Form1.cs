using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace Lab7__Threads_
{
    public partial class Form1 : Form
    {
        //public Bitmap MyTank=Resource1.TankUnusual, OtherTank1=Resource1.TankUsual, OtherTank2=Resource1.TankUsual;
        UsualTank t1, t2, t3;
        public static List<UsualTank> tanks = new List<UsualTank>();
        public static List<Ammo> ammos = new List<Ammo>();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Thread.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Ammo p in ammos)
                p.Move();
            Refresh();
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            Size PicSize = new Size(this.Size.Width - 40, this.Size.Height - 40);
            t1 = new UsualTank(new Point(0, 0), PicSize);
            t2 = new UsualTank(new Point(100, 100), PicSize);
            t3 = new UsualTank(new Point(300, 400), PicSize);
            tanks.Add(t1);
            tanks.Add(t2);
            tanks.Add(t3);
            Thread tank1 = new Thread(new ThreadStart(t1.Play));
            tank1.IsBackground = true;
            tank1.Start();
            Thread tank2 = new Thread(new ThreadStart(t2.Play));
            tank2.IsBackground = true;
            tank2.Start();
            Thread tank3 = new Thread(new ThreadStart(t3.Play));
            tank3.IsBackground = true;
            tank3.Start();
        }
        private object obj = new object();
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //g.DrawImage(MyTank, new Rectangle(0,0,40,40));
            /*if (!t1.Died)
                g.DrawImage(t1.TankView, new Rectangle(t1.position.X, t1.position.Y, 40, 40));
            if (!t2.Died)
                g.DrawImage(t2.TankView, new Rectangle(t2.position.X, t2.position.Y, 40, 40));
            if (!t3.Died)
                g.DrawImage(t3.TankView, new Rectangle(t3.position.X, t3.position.Y, 40, 40));*/
            ImageExec.DrawTanks(g, tanks);
            ImageExec.ShowAllAmmos(g, ammos);
            //UsualTank t = new UsualTank();
        }


    }
}
