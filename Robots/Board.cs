using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    class Board
    {
        private FType[,] _b;
        private List<Robot> list;
        private Dictionary<string, Robot> dict;
        private Random _rand;
        private Robot Player;

        public Board()
        {
            _rand = new Random();
            Level = 0;
            Score = 0;
            Player = null;
            dict = new Dictionary<string, Robot>();
            list = new List<Robot>();
            _b = new FType[Environment.Field_Y, Environment.Field_X];
        }

        private void InitWall()
        {
            for (int i = 0; i < Environment.Field_Y; i++)
            {
                for (int j = 0; j < Environment.Field_X; j++)
                {
                    bool isY = i % (Environment.Field_Y - 1) == 0;
                    bool isX = j % (Environment.Field_X - 1) == 0;
                    if (isY && isX)
                    {
                        this[i, j] = FType.Wall_Cross;
                    }
                    else if (isY)
                    {
                        this[i, j] = FType.Wall_Horizonal;
                    }
                    else if (isX)
                    {
                        this[i, j] = FType.Wall_Vertical;
                    }
                    else
                    {
                        this[i, j] = FType.None;
                    }
                }
            }
        }

        public void InitSpawn()
        {
            InitWall();
            list.Clear();
            dict.Clear();
            for (int i = 0; i < SpawnCount + 1; i++)
            {
                int xr = _rand.Next(Environment.Field_X - 2);
                int yr = _rand.Next(Environment.Field_Y - 2);

                while(this[yr+1, xr+1] != FType.None)
                {
                    xr++;
                    if (xr > Environment.Field_X - 2)
                    {
                        xr = 0;
                        yr++;
                    }
                    if (yr > Environment.Field_Y - 2)
                    {
                        yr = 0;
                    }
                }

                bool isPlayer = i == 0;
                this[yr + 1, xr + 1] = isPlayer ? FType.Player : FType.Robot;
                Robot r = isPlayer ? new Player(xr + 1, yr + 1) : new Robot(xr + 1, yr + 1);
                list.Add(r);
                dict.Add(r.CreateKey(), r);
            }
            Player = list[0];
        }

        public FType this[int i, int j]
        {
            private set
            {
                _b[i, j] = value;
            }

            get
            {
                return _b[i, j];
            }
        }

        public int Level
        {
            private set;
            get;
        }

        public int Score
        {
            private set;
            get;
        }

        public int LeftRobot
        {
            get { return list.Count() > 0 ? list.Count() - 1 : 0; }
        }

        public int RandX()
        {
            return _rand.Next(Environment.Field_X - 2);
        }

        public int RandY()
        {
            return _rand.Next(Environment.Field_Y - 2);
        }

        public void MoveRespawn(int x0, int y0, int x, int y)
        {
            string key = new Robot(x0, y0).CreateKey();
            string dis = new Robot(x, y).CreateKey();

            if (x < 1 || y < 1 || y >= Environment.Field_Y - 1 || x >= Environment.Field_X - 1) return;

            if (this[y, x] == FType.Wall_Cross || this[y, x] == FType.Wall_Horizonal || this[y, x] == FType.Wall_Vertical) return;

            if (dict.ContainsKey(key))
            {
                if (dict.ContainsKey(dis)) //Robot同士の衝突
                {
                    dict.Remove(dis);
                    dict.Remove(key);

                    Score += (this[y, x] != FType.Player && this[y0, x0] != FType.Player) ? 3 * Level : 0;

                    this[y, x] = FType.Scrap;
                    this[y0, x0] = FType.None;
                }
                else if (this[y, x] == FType.Scrap) //到着先がScrap
                {
                    dict.Remove(key);
                    this[y0, x0] = FType.None;

                    Score += (this[y, x] != FType.Player && this[y0, x0] != FType.Player) ? 1 * Level : 0;
                }
                else //セーフ
                {
                    //Console.WriteLine("Remove:{0} Add:{1}", key, new Robot(x, y).CreateKey());

                    this[y, x] = this[y0, x0] == FType.Player ? FType.Player : FType.Robot;
                    this[y0, x0] = FType.None;

                    var r = dict[key];
                    dict.Remove(key);
                    dict.Add(dis, r);
                }
            }
        }

        public bool Update(bool doUpdate = true)
        {
            if (doUpdate)
            {
                foreach (var r in list)
                {
                    r.Move(this, list[0]);
                }

                list.Clear();
                foreach (var v in dict)
                {
                    list.Add(v.Value);
                }
            }

            if (LeftRobot == 0)
            {
                NextLevel();
            }

            return list.Contains(Player);
        }

        public void NextLevel()
        {
            Level++;
            //Score = 0;
            InitSpawn();
        }

        public void PrintBoard()
        {
            for (int i = 0; i < Environment.Field_Y; i++)
            {
                for (int j = 0; j < Environment.Field_X; j++)
                {
                    Console.Write("{0}", this[i, j].PersonalChar);
                }
                Console.WriteLine();
            }
        }

        public void PrintInfo()
        {
            Console.WriteLine("Level:{0} Score:{1}?", Level, Score);
        }

        private int SpawnCount
        {
            get
            {
                return Level * 5 < 40 ? Level * 5 : 40;
            }
        }
    }
}
