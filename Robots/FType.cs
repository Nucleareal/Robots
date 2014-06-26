using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class FType
    {
        private FType(char personal, bool canMove)
        {
            PersonalChar = personal;
            CanMove = canMove;
        }

        public char PersonalChar
        {
            private set;
            get;
        }

        public bool CanMove
        {
            private set;
            get;
        }

        public static FType None = new FType(' ', true);
        public static FType Wall_Cross = new FType('+', false);
        public static FType Wall_Vertical = new FType('|', false);
        public static FType Wall_Horizonal = new FType('-', false);
        public static FType Scrap = new FType('*', true);
        public static FType Robot = new FType('+', true);
        public static FType Player = new FType('@', true);
    }
}
