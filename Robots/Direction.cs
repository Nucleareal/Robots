using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Direction
    {
        private Direction(int dx, int dy, char pChar)
        {
            DiffX = dx;
            DiffY = dy;
            PersonalChar = pChar;
        }

        public char PersonalChar
        {
            private set;
            get;
        }

        public int DiffX
        {
            private set;
            get;
        }

        public int DiffY
        {
            private set;
            get;
        }

        public static Direction Match(char pchar)
        {
            IEnumerable<Direction> l = list.Where(x => x.PersonalChar == pchar);
            if (l.Count() <= 0) return null;
            return l.First();
        }

        public static Direction YNegXNeg = new Direction(-1, -1, '7');
        public static Direction YNegXNeu = new Direction(-1, +0, '4');
        public static Direction YNegXPos = new Direction(-1, +1, '1');
        public static Direction YNeuXPos = new Direction(+0, +1, '2');
        public static Direction YPosXPos = new Direction(+1, +1, '3');
        public static Direction YPosXNeu = new Direction(+1, +0, '6');
        public static Direction YPosXNeg = new Direction(+1, -1, '9');
        public static Direction YNeuXNeg = new Direction(+0, -1, '8');
        public static Direction YNeuXNeu = new Direction(+0, +0, '5');
        public static List<Direction> list;

        static Direction()
        {
            list = new List<Direction>();
            list.Add(YNegXNeg);
            list.Add(YNegXNeu);
            list.Add(YNegXPos);
            list.Add(YNeuXPos);
            list.Add(YPosXPos);
            list.Add(YPosXNeu);
            list.Add(YPosXNeg);
            list.Add(YNeuXNeg);

            list.Add(YNeuXNeu);
        }
    }
}
