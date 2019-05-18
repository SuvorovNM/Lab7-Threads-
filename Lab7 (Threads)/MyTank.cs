using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Lab7__Threads_
{
    class MyTank: Tank
    {
        public MyTank(Point _position, Size _bg)
        {
            direction = Dir.North;
            TankView = Resource1.TankUnusual;
            position = _position;
            background = _bg;
            state = State.Free;
        }
    }
}
