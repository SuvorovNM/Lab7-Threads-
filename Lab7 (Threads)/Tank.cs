using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Lab7__Threads_
{
    public class Tank
    {
        public Bitmap TankView;
        public State state;
        protected Dir direction;
        public Point position;
        protected Size background;
        public bool Died = false;
        public int Kills = 0;
        private object ThreadLock = new object();

        public void TurnRight()
        {
            lock (ThreadLock)
            {
                if (direction != Dir.West)
                {
                    direction++;
                }
                else
                {
                    direction = Dir.North;
                }
                if (state != State.Free)
                    do
                    {
                        Thread.Sleep(20);
                    } while (state != State.Free);
                state = State.OnWrite;
                TankView = ImageExec.Rotate90(TankView);
                state = State.Free;
            }
        }
        public void TurnLeft()
        {
            lock (ThreadLock)
            {
                if (direction != Dir.North)
                {
                    direction--;
                }
                else
                {
                    direction = Dir.West;
                }
                if (state != State.Free)
                    do
                    {
                        Thread.Sleep(20);
                    } while (state != State.Free);
                state = State.OnWrite;
                TankView = ImageExec.Rotate270(TankView);
                state = State.Free;
                //TankView.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
        }

        private bool CorrectMove(int x, int y, Dir direction)
        {
            bool OK = true;
            foreach (Tank t1 in Form1.tanks)
            {
                if (t1 != this)
                {
                    OK = OK && (Math.Abs(x - t1.position.X) > 20 || Math.Abs(y - t1.position.Y) > 20);

                }
            }
            return OK;
        }
        public void Move()
        {
            lock (ThreadLock)
                switch (direction)
                {
                    case Dir.East:
                        if (position.X + 40 < background.Width && CorrectMove(position.X + 20, position.Y, direction))
                            position.X += 20;
                        break;
                    case Dir.North:
                        if (position.Y > 0 && CorrectMove(position.X, position.Y - 20, direction))
                            position.Y -= 20;
                        break;
                    case Dir.West:
                        if (position.X > 0 && CorrectMove(position.X - 20, position.Y, direction))
                            position.X -= 20;
                        break;
                    case Dir.South:
                        if (position.Y + 40 < background.Height && CorrectMove(position.X, position.Y + 20, direction))
                            position.Y += 20;
                        break;
                }
        }
        public void Shot()
        {
            Point posForAmmo;
            if (direction == Dir.East || direction == Dir.West)
                posForAmmo = new Point(position.X + 25, position.Y + 15);
            else
                posForAmmo = new Point(position.X + 15, position.Y + 25);
            Ammo Shot = new Ammo(direction, posForAmmo, background, this);
            //Shot.Play();
            Form1.ammos.Add(Shot);
            //Thread.Sleep(200);
        }
    }
}
