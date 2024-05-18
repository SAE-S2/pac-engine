using pac_engine.Utils;
using System.Text;

namespace pac_engine.Core
{
	public class Game
    {
        private bool playing = true;
        public bool Playing
        {
            get { return playing; }
        }
        private Map map;
		public Player player;
        private int enemiesNum = 0;
        private Entity[] enemies = new Entity[10]; // TODO: Change 10???
        private bool win = false;

        public Game(ref Player playerRef)
		{
            player = playerRef;
        }

        public bool Start()
        {
            int[,] tempMap = CreateMap();
            for (int i = 0; i < tempMap.GetLength(0); i++)
                for (int j = 0; j < tempMap.GetLength(1); j++)
                    if (tempMap[i, j] == 5)
                    {
                        enemies[enemiesNum] = new Guard();
                        enemies[enemiesNum].angle = 3;
                        enemies[enemiesNum].pos = new Vector2(i, j);
                        enemies[enemiesNum].SetActualGame(this);
                        enemiesNum++;
                    }

            map = new Map(tempMap, this);
            player.pos = map.spawn;
            player.SetActualGame(this);
            player.Movement(map);

            for (int i = 0; i < enemiesNum; i++)
                enemies[i].Movement(map);

            Print();
            while (playing)
            {
                ConsoleKey input = Console.ReadKey().Key;

                if (input == ConsoleKey.Z)
                {
                    player.AngleChange(0);
                }
                else if (input == ConsoleKey.Q)
                {
                    player.AngleChange(3);
                }
                else if (input == ConsoleKey.S)
                {
                    player.AngleChange(2);
                }
                else if (input == ConsoleKey.D)
                {
                    player.AngleChange(1);
                }
                else if (input == ConsoleKey.Spacebar)
                {
                    player.AngleChange(4);
                }
                else if (input == ConsoleKey.A)
                {
                    player.ActivePower();
                }
            }

            return win;
        }

        public void PlayerDied()
        {
            playing = false;
        }

        public void EnemyDie(Entity enemy)
        {
            for (int i = 0; i < enemiesNum; i++)
            {
                if (enemies[i] == enemy)
                {
                    enemiesNum--;
                    if (i < enemiesNum)
                    {
                        for (int j = i; j < enemiesNum; j++)
                        {
                            enemies[j] = enemies[j+1];
                        }
                    }
                }
            }
        }

        public void PlayerAtDoor()
        {
            win = true;
            playing = false;
        }

        private int[,] CreateMap()
        {
            // TODO: Implement algo

            int[,] map = {
                {
                    1, 1, 1, 1, 1, 1, 1, 1, 1
                },
                {
                    1, 0, 0, 0, 0, 0, 5, 0, 1
                },
                {
                    1, 0, 0, 1, 0, 1, 1, 0, 1
                },
                {
                    1, 1, 0, 3, 0, 0, 0, 0, 1
                },
                {
                    1, 0, 0, 1, 0, 1, 1, 0, 1
                },
                {
                    1, 1, 1, 1, 2, 1, 1, 1, 1
                }
            };

            return map;
        }

        public (int[,], float, float, int, int) GetInfo()
        {
            return (map.map, player.Health, player.absorption, player.bolts, player.money);
        }

        public Entity EnemyAtPos(Vector2 pos)
        {
            Entity enemy = player;
            for (int e = 0; e < enemiesNum && enemiesNum != 0; e++)
                if (enemies[e].pos.x == pos.x && pos.y == enemies[e].pos.y)
                    enemy = enemies[e];

            return enemy;
        }

        // TODO: Delete debug function 
        public async Task Print()
        {
            int[,] map;
            float health, absorption;
            int bolts, money;

            await Task.Run(() =>
            {
                while (playing)
                {
                    (map, health, absorption, bolts, money) = GetInfo();

                    Console.WriteLine(health + " HP /  " + absorption + " Shield /  " + bolts + " Bolts  /  " + money + " Coins");
                    Console.WriteLine();

                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            if (i == player.pos.x && j == player.pos.y)
                            {
                                Console.OutputEncoding = Encoding.UTF8;
                                Console.ForegroundColor = ConsoleColor.Green;
                                switch (player.angle)
                                {
                                    case 0:
                                        Console.Write("\u25B2 ");
                                        break;
                                    case 1:
                                        Console.Write("\u25B6 ");
                                        break;
                                    case 2:
                                        Console.Write("\u25BC ");
                                        break;
                                    case 3:
                                        Console.Write("\u25C0 ");
                                        break;
                                    case 4:
                                        Console.Write("+ ");
                                        break;
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                bool enemieAtPos = false;
                                for (int e = 0; e < enemiesNum && enemiesNum != 0; e++)
                                    if (enemies[e].pos.x == i && j == enemies[e].pos.y)
                                        enemieAtPos = true;
                                if (map[i, j] == 2 || map[i, j] == 3)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                if (map[i, j] == 0)
                                    Console.ForegroundColor = ConsoleColor.Black;
                                if (enemieAtPos)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(enemieAtPos ? "\u2603 " : map[i, j] + " ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    Task.Delay(150).Wait();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            });
        }
    }
}

