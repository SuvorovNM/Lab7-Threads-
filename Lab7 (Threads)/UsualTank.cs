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
    public enum State { Free, OnRead, OnWrite }
    public class UsualTank : Tank
    {
        private static Random rng = new Random();
        public UsualTank(Point _position, Size _bg)
        {
            direction = Dir.North;
            TankView = Resource1.TankUsual;
            position = _position;
            background = _bg;
            state = State.Free;
        }
        public void Play()
        {
            while (!Died)
            {
                foreach (Ammo p in Form1.ammos.ToArray())
                {
                    if (p!=null&&!p.Hit && !Died &&p.Shooter!=this && (p.position.X - this.position.X) < 30 && (p.position.X - this.position.X) > -10 && (p.position.Y - this.position.Y) < 30 && (p.position.Y - this.position.Y) > -10)
                    {
                        Died = true;
                        p.Hit = true;
                        //INTERLOCKED INCREMENT
                        p.Shooter.Kills = Interlocked.Increment(ref p.Shooter.Kills);
                        Thread.CurrentThread.Abort();
                    }
                }
                int turn = rng.Next(0, 8);
                if (turn == 0)
                    TurnLeft();
                if (turn == 1)
                    TurnRight();
                if (turn >=3 && turn <=5)
                {
                    Shot();
                    //Thread.Sleep(200);
                }
                else Move();
                Thread.Sleep(200);
            }
            if (Died)
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}
