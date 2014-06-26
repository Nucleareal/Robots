using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Player : Robot
    {
        public Player(int x, int y) : base(x, y)
        {
            Axis.LimitMode = true;
        }

        public override Robot Move(Board b, Robot target)
        {
            char c = Console.ReadKey().KeyChar;

            if (c == '0')
            {
                int rx = b.RandX();
                int ry = b.RandY();

                while (b[ry+1, rx+1] != FType.None)
                {
                    rx++;
                    if (rx > Environment.Field_X - 2)
                    {
                        rx = 0;
                        ry++;
                    }
                    if (ry > Environment.Field_Y - 2)
                    {
                        ry = 0;
                    }
                }

                rx += 1;
                ry += 1;

                b.MoveRespawn(Axis.X, Axis.Y, rx, ry);
                Axis.X = rx;
                Axis.Y = ry;
            }
            else
            {
                var v = Direction.Match(c);
                if (v != null)
                {
                    int x = Axis.X;
                    int y = Axis.Y;

                    Axis.X += v.DiffX;
                    Axis.Y += v.DiffY;

                    if(x != Axis.X || y != Axis.Y)
                        b.MoveRespawn(x, y, Axis.X, Axis.Y);
                }
            }

            return b[Axis.Y, Axis.X] != FType.None ? this : null;
        }

        public override bool IsPlayer
        {
            get
            {
                return true;
            }
        }

        public override char PersonalChar
        {
            get
            {
                return '@';
            }
        }
    }
}
