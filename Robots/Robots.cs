using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Robots
    {
        static void Main(string[] args)
        {
            Environment.Field_X = 62;
            Environment.Field_Y = 22;

            Game g = new Game();
            g.GameLoop();
        }
    }
}
