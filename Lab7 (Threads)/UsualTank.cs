using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Lab7__Threads_
{

    public enum Dir { North, East, South, West }
    public enum State {Free, OnRead, OnWrite }
    public class UsualTank
    {        
        public Bitmap TankView;
        public State state;
        Dir direction;
        public Point position;
        Size background;
        private static Random rng = new Random();
        public bool Died = false;
        private object ThreadLock = new object();        

        public Bitmap TankImage
        {            
            get
            {
                lock (ThreadLock)
                    return TankView;
            }
            set
            {
                lock (ThreadLock)
                    TankView = value;
            }
        }

        public UsualTank(Point _position, Size _bg)
        {
            direction = Dir.North;
            TankView = Resource1.TankUsual;
            position = _position;
            background = _bg;
            state = State.Free;
        }

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
                //TankView.RotateFlip(RotateFlipType.Rotate90FlipNone);
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

        public void Move()
        { 
            lock (ThreadLock)
            switch (direction)
            {                
                case Dir.East:
                    if (position.X + 40 < background.Width && CorrectMove(position.X+20,position.Y,direction))
                        position.X += 20;
                    break;
                case Dir.North:
                    if (position.Y > 0 && CorrectMove(position.X, position.Y-20, direction))
                        position.Y -= 20;
                    break;
                case Dir.West:
                    if (position.X > 0 && CorrectMove(position.X - 20, position.Y, direction))
                        position.X -= 20;
                    break;
                case Dir.South:
                    if (position.Y + 40 < background.Height && CorrectMove(position.X, position.Y+20, direction))
                        position.Y += 20;
                    break;
            }
        }
        private bool CorrectMove(int x, int y, Dir direction)
        {
            bool OK = true;
            foreach (UsualTank t1 in Form1.tanks)
            {
                if (t1 != this)
                {
                    OK = OK && (Math.Abs(x - t1.position.X) > 20 || Math.Abs(y - t1.position.Y) > 20);
                    /*switch (direction)
                    {
                        case Dir.East:
                            
                            break;
                    }*/

                }
            }
            return OK;
        }
        public void Play()
        {
            while (!Died)
            {
                foreach (Ammo p in Form1.ammos.ToArray())
                {
                    if (!p.Hit && (p.position.X - this.position.X) < 30 && (p.position.X - this.position.X) > -10 && (p.position.Y - this.position.Y) < 30 && (p.position.Y - this.position.Y) > -10)
                    {
                        Died = true;
                        p.Hit = true;
                        Thread.CurrentThread.Abort();
                    }
                }
                int turn = rng.Next(0, 4);
                if (turn == 0)
                    TurnLeft();
                if (turn == 1)
                    TurnRight();
                if (turn == 3)
                {
                    lock (this)
                    {
                        Point posForAmmo;
                        if (direction == Dir.East || direction == Dir.West)
                            posForAmmo = new Point(position.X + 25, position.Y + 15);
                        else
                            posForAmmo = new Point(position.X + 15, position.Y + 25);
                        Ammo Shot = new Ammo(direction, posForAmmo, background);
                        //Shot.Play();
                        Form1.ammos.Add(Shot);
                    }
                    Thread.Sleep(200);
                }
                else Move();
                Thread.Sleep(200);
            }
            if (Died)
            {
                Thread.CurrentThread.Abort();
            }
        }
        private static object TL = new object();
        
    }
}
