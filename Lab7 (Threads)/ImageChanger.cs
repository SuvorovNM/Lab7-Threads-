using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using System.Drawing;

namespace Lab7__Threads_
{
    [Synchronization]
    class ImageExec : ContextBoundObject
    {
        public static void DrawTanks(Graphics g, List<UsualTank> tanks)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            foreach (UsualTank t1 in tanks.ToArray())
            {
                if (t1.Died)
                {
                    //t1.TankView.
                    for (int i = 0; i < 80; i++)
                    {
                        for (int j = 0; j < 80; j++)
                            t1.TankView.SetPixel(i, j, Color.Red);
                    }
                }
                if (t1.state != State.Free)
                    do
                    {
                        Thread.Sleep(20);
                    } while (t1.state != State.Free);
                t1.state = State.OnRead;
                Bitmap bmp = new Bitmap(t1.TankImage);
                t1.state = State.Free;
                g.DrawImage(bmp, new Rectangle(t1.position.X, t1.position.Y, 40, 40));
            }
        }
        public static void ShowAllAmmos(Graphics g, List<Ammo> ammos)
        {
            foreach (Ammo p in ammos.ToArray())
            {
                if (!p.Hit)
                {
                    g.DrawImage(p.image, new Rectangle(p.position.X, p.position.Y, 10, 10));
                }
                else ammos.Remove(p);
            }
        }
        public static Bitmap Rotate90(Bitmap bmp)
        {
            Bitmap tmp = new Bitmap(bmp);
            tmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return tmp;
        }
        public static Bitmap Rotate270(Bitmap bmp)
        {
            Bitmap tmp = new Bitmap(bmp);
            tmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return tmp;
        }
    }
}
