using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace Lab7__Threads_
{
    [Synchronization]
    public class Ammo
    {
        Dir direction;
        public Bitmap image;
        public Point position;
        Size background;
        public bool Hit = false;
        public bool Kill = false;
        public Tank Shooter;
        public Ammo(Dir _dir, Point _pos, Size _bg, Tank _shooter)
        {
            image = Resource1.Ammo;
            position = _pos;
            direction = _dir;
            if (direction==Dir.North|| direction == Dir.South)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            background = _bg;
            Shooter = _shooter;
        }
        public void Move()
        {
            switch (direction)
            {
                case Dir.East:
                    if (position.X + 20 < background.Width)
                        position.X += 20;
                    else Hit = true;
                    break;
                case Dir.North:
                    if (position.Y > 0)
                        position.Y -= 20;
                    else Hit = true;
                    break;
                case Dir.West:
                    if (position.X > 0)
                        position.X -= 20;
                    else Hit = true;
                    break;
                case Dir.South:
                    if (position.Y + 20 < background.Height)
                        position.Y += 20;
                    else Hit = true;
                    break;
            }
        }
        
    }
}
