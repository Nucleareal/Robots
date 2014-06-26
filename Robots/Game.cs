using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Game
    {
        private Board _b;

        public Game()
        {
            Init();
        }

        public void Init()
        {
            GameAlive = true;
            InitBoard();
        }

        public void InitBoard()
        {
            _b = new Board();
        }

        private bool GameAlive
        {
            set;
            get;
        }

        //具体的にゲームの処理をします
        public void GameLoop()
        {
            _b.Update(false);

            while (GameAlive)
            {
                Console.Clear();
                _b.PrintBoard();
                _b.PrintInfo();
                GameAlive = _b.Update();
            }

            Console.Clear();
            Console.WriteLine("G A M E   O V E R \nSCORE:{0}", _b.Score);
            Console.WriteLine("Press Any Key to Quit");
            Console.ReadKey();
        }
    }
}
