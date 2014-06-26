using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Robot
    {
        public Robot(int x, int y)
        {
            Axis = new Point(x, y);
        }

        protected Point Axis
        {
            private set;
            get;
        }

        public string CreateKey()
        {
            return string.Format("{0},{1}", Axis.X, Axis.Y);
        }

        public virtual Robot Move(Board b, Robot target)
        {
            int dx = this.Axis.X > target.Axis.X ? -1 : this.Axis.X < target.Axis.X ? 1 : 0;
            int dy = this.Axis.Y > target.Axis.Y ? -1 : this.Axis.Y < target.Axis.Y ? 1 : 0;

            b.MoveRespawn(Axis.X, Axis.Y, Axis.X+dx, Axis.Y+dy);

            Axis.X += dx;
            Axis.Y += dy;

            return b[Axis.Y, Axis.X] != FType.None ? this : null; ;
        }

        public virtual bool IsPlayer
        {
            get
            {
                return false;
            }
        }

        public virtual char PersonalChar
        {
            get
            {
                return '+';
            }
        }
    }
}
