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
using System.Diagnostics;

namespace Lab7__Threads_
{
    public partial class Form1 : Form
    {
        //public Bitmap MyTank=Resource1.TankUnusual, OtherTank1=Resource1.TankUsual, OtherTank2=Resource1.TankUsual;
        UsualTank t1, t2, t3;
        MyTank UserTank;
        public static List<Tank> tanks = new List<Tank>();
        public static List<Ammo> ammos = new List<Ammo>();
        private List<Thread> MyBGThreads = new List<Thread>();
        bool Wait = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void StartGame()
        {
            //Уничтожение потоков
            foreach (Thread t in MyBGThreads.ToArray())
            {
                t.Abort();
                MyBGThreads.Remove(t);
            }            
            Wait = false;
            tanks = new List<Tank>();
            ammos = new List<Ammo>();
            Size PicSize = new Size(this.Size.Width - 40, this.Size.Height - 40);
            t1 = new UsualTank(new Point(100, 100), PicSize);
            t2 = new UsualTank(new Point(200, 200), PicSize);
            t3 = new UsualTank(new Point(500, 400), PicSize);
            UserTank = new MyTank(new Point(200, 250), PicSize);
            tanks.Add(t1);
            tanks.Add(t2);
            tanks.Add(t3);
            tanks.Add(UserTank);
            //Создание потоков:
            Thread tank1 = new Thread(new ThreadStart(t1.Play));
            tank1.IsBackground = true;
            tank1.Priority = ThreadPriority.Highest;
            tank1.Start();
            Thread tank2 = new Thread(new ThreadStart(t2.Play));
            tank2.Priority = ThreadPriority.Lowest;
            tank2.IsBackground = true;
            tank2.Start();
            Thread tank3 = new Thread(new ThreadStart(t3.Play));
            tank3.Priority = ThreadPriority.Lowest;
            tank3.IsBackground = true;
            tank3.Start();
            Thread.CurrentThread.Priority = ThreadPriority.Highest;         
            MyBGThreads.Add(tank1);
            MyBGThreads.Add(tank2);
            MyBGThreads.Add(tank3);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Ammo p in ammos)
                p.Move();            
            if (!Wait)
                foreach (Ammo p in ammos.ToArray())
                {
                    if (Wait) break;
                    if (!p.Hit && p.Shooter != UserTank && (p.position.X - UserTank.position.X) < 30 && (p.position.X - UserTank.position.X) > -10 && (p.position.Y - UserTank.position.Y) < 30 && (p.position.Y - UserTank.position.Y) > -10)
                    {
                        Wait = true;
                        UserTank.Died = true;
                        p.Hit = true;
                        int CountKills = 0;
                        CountKills = UserTank.Kills;
                        Results rs = new Results();
                        Results.Result = CountKills;
                        Results.Victory = false;
                        rs.ShowDialog();

                        if (Results.Restart)
                        {
                            StartGame();
                        }
                        else Close();
                    }
                    else if (tanks.Where(tn => tn.Died).Count() >= tanks.Count - 1)
                    {
                        Wait = true;
                        int CountKills = 0;
                        CountKills = UserTank.Kills;
                        Results rs = new Results();
                        Results.Result = CountKills;
                        Results.Victory = true;
                        rs.ShowDialog();
                        if (Results.Restart)
                        {
                            StartGame();
                        }
                        else Close();
                    }                    
                }
            Refresh();
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            StartGame();
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == 38)
            {
                UserTank.Move();
            }
            else if (e.KeyChar == 37)
            {
                UserTank.TurnLeft();
            }
            else if (e.KeyChar == 39)
            {
                UserTank.TurnRight();
            }
            else if (e.KeyChar == 32)
            {
                UserTank.Shot();
            }*/
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!UserTank.Died)
                if (e.KeyCode == Keys.Up)
                {
                    UserTank.Move();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    UserTank.TurnLeft();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    UserTank.TurnRight();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    UserTank.Shot();
                }
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
