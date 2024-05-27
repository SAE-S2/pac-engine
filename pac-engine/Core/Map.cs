using pac_engine.Utils;
using System.ComponentModel.Design;

namespace pac_engine.Core
{
    public class Map
    {
        public Vector2 spawn;
        public int[,] map;
        private int coinsNumber = 0;
        private int coinsEarn = 0;
        public Vector2 door;
        public event EventHandler<EarnCoinEventArgs> CoinEarn;
        public event EventHandler<DoorOpenEventArgs> DoorOpen;

        protected void CoinEarnEvent(Vector2 pos)
        {
            CoinEarn?.Invoke(this, new EarnCoinEventArgs { Pos = pos });
        }

        protected void DoorOpenEvent(Vector2 pos)
        {
            DoorOpen?.Invoke(this, new DoorOpenEventArgs { DoorPos = pos });
        }

        public Map(int[,] mapToLoad, Game game)
        {
            map = mapToLoad;
            Random random = new Random();

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] == 0 || map[i, j] == 5)
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
        }

        public bool EarnBolt(Vector2 pos)
        {
            if (GetBolt(pos))
            {
                CoinEarnEvent(pos);
                map[pos.x, pos.y] = 0;
                return true;
            }
            return false;
        }

        public bool EarnCoin(Vector2 pos)
        {
            if (map[pos.x, pos.y] == 2)
            {
                CoinEarnEvent(pos);
                map[pos.x, pos.y] = 0;
                coinsEarn++;
                if (coinsEarn > coinsNumber * 0.75)
                {
                    map[door.x, door.y] = 6;
                    DoorOpenEvent(door);
                }
                return true;
            }
            return false;
        }

        public int GetWallType(int y, int x)
        {
            int nbVoid = 0;
            int maxY = map.GetLength(0);
            int maxX = map.GetLength(1);
            // Bordures de map

            // Door
            if (door.y == 0 || door.y == maxY - 1)
            {
                if (x == door.x - 1) return 14;
                else if (x == door.x + 1) return 12;
            }

            if (door.x == 0 || door.x == maxX - 1)
            {
                if (y == door.y - 1) return 11;
                else if (y == door.y + 1) return 13;
            }

            // Bordure haut
            if (y == 0)
            {
                if (x == 0)
                {
                    return 8; //Droite-Bas
                }
                else if (x == maxX - 1)
                {
                    return 9; //Gauche-Bas
                }
                else
                {
                    if (!GetVoid(y + 1, x)) { return 13; } // Droite-Bas-Gauche
                    return 5; //Droite-Gauche (Horizontal)
                }
            }

            //Bordure Bas
            else if (y == maxY - 1)
            {
                if (x == 0)
                {
                    return 7; //Haut-Droite
                }
                else if (x == maxX - 1)
                {
                    return 10; //Haut-Gauche
                }
                else
                {
                    if (!GetVoid(y - 1, x)) { return 11; } // Droite-Haut-Gauche
                    return 5; //Droite-Gauche (Horizontal)
                }
            }

            // Bordure gauche
            else if (x == 0)
            {
                if (!GetVoid(y, x + 1)) { return 12; } //Haut-Droite-Bas
                return 6; // Haut-Bas (Vertical)
            }

            // Bordure droite
            else if (x == 0 || x == maxX - 1)
            {
                if (!GetVoid(y, x - 1)) { return 14; } //Haut-Gauche-Bas
                return 6; // Haut-Bas (Vertical)
            }

            // Intérieur de la map

            else
            {
                bool haut = GetVoid(y - 1, x);
                bool bas = GetVoid(y + 1, x);
                bool gauche = GetVoid(y, x - 1);
                bool droite = GetVoid(y, x + 1);
                if (haut) { nbVoid++; }
                if (bas) { nbVoid++; }
                if (gauche) { nbVoid++; }
                if (droite) { nbVoid++; }

                switch (nbVoid)
                {
                    case 4: // Mur à 0 embranchement
                        return 0;
                    case 3: // Mur à 1 embranchement
                        if (!haut) { return 1; }
                        else if (!droite) { return 2; }
                        else if (!bas) { return 3; }
                        else if (!gauche) { return 4; }
                        break;
                    case 2: // Mur à 2 embranchements
                        if (haut && bas) { return 5; }
                        else if (gauche && droite) { return 6; }
                        else if (bas && gauche) { return 7; }
                        else if (gauche && haut) { return 8; }
                        else if (haut && droite) { return 9; }
                        else if (droite && bas) { return 10; }
                        break;
                    case 1: // Mur à 3 embranchement
                        if (bas) { return 11; }
                        else if (gauche) { return 12; }
                        else if (haut) { return 13; }
                        else if (droite) { return 14; }
                        break;
                    case 0: // Mur à 4 embranchements
                        return 15; //Haut-Gauche-Bas-Droite vide
                }
                return 16; // Erreur
            }
        }

        public bool GetVoid(int x, int y)
        {
            return (GetBolt(x, y) || GetCoin(x, y) || map[x, y] == 0 || GetDoor(x, y));
        }

        public bool GetBolt(Vector2 pos)
        {
            return (map[pos.x, pos.y] == 3);
        }

        public bool GetBolt(int x, int y)
        {
            return (map[x, y] == 3);
        }

        public bool GetCoin(int x, int y)
        {
            return (map[x, y] == 2);
        }

        public bool GetWall(int x, int y)
        {
            return (map[x, y] == 1 || map[x, y] == 4 || map[x, y] == 6);
        }

        public bool GetDoor(int x, int y)
        {
            return (map[x, y] == 4);
        }
    }
}