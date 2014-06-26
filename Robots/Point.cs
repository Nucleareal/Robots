using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool LimitMode
        {
            set;
            private get;
        }

        private int _x;
        private int _y;

        public int X
        {
            set
            {
                _x = value;
                if (LimitMode)
                {
                    if (_x <= 0) _x = 1;
                    if (Environment.Field_X - 1 <= _x) _x = Environment.Field_X - 2;
                }
            }
            get
            {
                return _x;
            }
        }

        public int Y
        {
            set
            {
                _y = value;
                if (LimitMode)
                {
                    if (_y <= 0) _y = 1;
                    if (Environment.Field_Y - 1 <= _y) _y = Environment.Field_Y - 2;
                }
            }
            get
            {
                return _y;
            }
        }
    }
}
