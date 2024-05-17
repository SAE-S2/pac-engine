using pac_engine.Utils;

namespace pac_engine.Core
{
    public class Map
    {
        public Vector2 spawn;
        public int[,] map;
        private int coinsNumber = 0;
        private int coinsEarn = 0;
        public Vector2 door;

        public Map(int[,] mapToLoad, Game game)
        {
            map = mapToLoad;
            Random random = new Random();
            int BoltCount = 0;

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] == 0 || map[i, j] == 5)
                    {
                        int isBolts = random.Next(1, 25);
                        if (isBolts == 5 && BoltCount<=3)
                        {
                            map[i, j] = 3;
                            BoltCount++;
                        }
                        else 
                        {
                            coinsNumber++;
                            map[i, j] = 2;
                        }
                    }
                    else if (map[i, j] == 2)
                    {
                        spawn = new Vector2(i, j);
                        map[i, j] = 0;
                    }
                    else if (map[i, j] == 4)
                    {
                        door = new Vector2(i, j);
                        map[i, j] = 4;
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
    }
}