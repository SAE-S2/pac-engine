using System.Text;
using pac_engine.Utils;

namespace pac_engine.Core
{
    public class Map
    {
        public Vector2 spawn;
        public Player player;
        public int[,] map;
        public int coinsNumber = 0;
        public int coinsEarn = 0;
        public Vector2 door;
        private int enemiesNum = 0;
        public Entity[] enemies = new Entity[10];

        public Map(int[,] mapToLoad, ref Player playerToLoad)
        {
            map = mapToLoad;
            player = playerToLoad;
            Random random = new Random();

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] == 0)
                    {
                        int isBolts = random.Next(1, 25);
                        if (isBolts == 5)
                        {
                            map[i, j] = 3;
                        }
                        else 
                        {
                            coinsNumber++;
                            map[i, j] = 2;
                        }
                    }
                    else if (map[i, j] == 3)
                    {
                        spawn = new Vector2(i, j);
                        map[i, j] = 0;
                    }
                    else if (map[i, j] == 2)
                    {
                        door = new Vector2(i, j);
                        map[i, j] = 4;
                    }
                    else if (map[i, j] == 5)
                    {
                        enemies[enemiesNum] = new Guard();
                        enemies[enemiesNum].angle = 3;
                        enemies[enemiesNum].pos = new Vector2(i, j);
                        enemies[enemiesNum].Movement(this);
                        enemiesNum++;
                        coinsNumber++;
                        map[i, j] = 2;
                    }
        }

        public bool GetBolts(Vector2 pos)
        {
            if (map[pos.x, pos.y] == 3)
            {
                map[pos.x, pos.y] = 0;
                return true;
            }
            return false;
        }

        public bool GetCoin(Vector2 pos)
        {
            if (map[pos.x, pos.y] == 2)
            {
                map[pos.x, pos.y] = 0;
                coinsEarn++;
                if (coinsEarn > coinsNumber*0.75)
                {
                    map[door.x, door.y] = 0;
                }
                return true;
            }
            return false;
        }

        public bool GetWall(Vector2 pos)
        {
            return (map[pos.x, pos.y] == 1 || map[pos.x, pos.y] == 4);
        }

        public bool GetWall(int x,int y)
        {
            return (map[x,y] == 1 || map[x, y] == 4);
        }

        public async Task Print()
        {
            await Task.Run(() =>
            {
                while (map != null)
                {
                    // Debug:
                    Console.WriteLine(player.Health + " HP  /  " + player.bolts + " Bolts  /  " + player.money + " Coins");
                    Console.WriteLine();
                    //

                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            if (i == player.pos.x && j == player.pos.y)
                            {
                                Console.OutputEncoding = Encoding.UTF8;
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
                                }

                                if (GetCoin(player.pos))
                                {
                                    player.money++;
                                }
                                else if (GetBolts(player.pos))
                                {
                                    player.bolts++;
                                }
                            }
                            else
                            {
                                bool enemiesAtPos = false;
                                for (int e = 0; e < enemiesNum && enemiesNum != 0; e++)
                                    if (enemies[e].pos.x == i && j == enemies[e].pos.y)
                                        enemiesAtPos = true;

                                Console.Write(enemiesAtPos ? "\u2603 " : map[i, j]+" ");
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