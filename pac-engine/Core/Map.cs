using pac_engine.Utils;

namespace pac_engine.Core
{
    public class Map
    {
        private int maxX, maxY;
        public Vector2 spawn;
        public int[,] map;

        public Map() 
        {
            maxX = 100; maxY = 100; spawn = (Vector2)new Vector2(50);
            map = new int[maxX, maxY];
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    if (i == 0 || j == 0 || i == maxX - 1 || j == maxY - 1) 
                    {
                        map[i, j] = 1; 
                    }
                    else 
                    { 
                        map[i, j] = 0; 
                    }
        }

        public Map(int xy)
        {
            maxX = xy; maxY = xy; spawn = (Vector2)new Vector2(xy/2);
            map = new int[maxX, maxY];
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    if (i == 0 || j == 0 || i == maxX-1 || j == maxY-1)
                    {
                        map[i, j] = 1;
                    }
                    else
                    {
                        map[i, j] = 0;
                    }
        }

        public Map(int x, int y) 
        {
                maxX = x; maxY = y; spawn = (Vector2)new Vector2(x/2,y/2);
            map = new int[maxX, maxY];
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    if (i == 0 || j == 0 || i == maxX - 1 || j == maxY - 1)
                    {
                        map[i, j] = 1;
                    }
                    else
                    {
                        map[i, j] = 0;
                    }
        }

        public Map(int x, int y, Vector2 spawn)
        {
            maxX = x; maxY = y; this.spawn = spawn;
            map = new int[maxX, maxY];
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    if (i == 0 || j == 0 || i == maxX - 1 || j == maxY - 1)
                    {
                        map[i, j] = 1;
                    }
                    else
                    {
                        map[i, j] = 0;
                    }
        }

        public bool GetCoin(Vector2 pos)
        {
            if (map[pos.x, pos.y] == 2)
            {
                map[pos.x, pos.y] = 0;
                return true;
            }
            return false;
        }

        public bool GetWall(Vector2 pos)
        {
            return map[pos.x, pos.y] == 1;
        }

        public bool GetWall(int x,int y)
        {
            return map[x,y] == 1;
        }

        public void PrintMap(Vector2 pos, Vector2 ennemie)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if (pos.x == i && pos.y == j)
                    {
                        Console.Write("X");
                    }
                    else if (ennemie.x == i && ennemie.y == j)
                    {
                        Console.Write("Y");
                    } 
                    else
                    {
                        Console.Write(map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

    }
}