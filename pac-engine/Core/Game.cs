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
                if (input == ConsoleKey.Q)
                {
                    player.AngleChange(3);
                }
                if (input == ConsoleKey.S)
                {
                    player.AngleChange(2);
                }
                if (input == ConsoleKey.D)
                {
                    player.AngleChange(1);
                }
                if (input == ConsoleKey.Spacebar)
                {
                    player.AngleChange(4);
                }
            }

            return win;
        }

        public void PlayerDied()
        {
            playing = false;
        }

        public void PlayerAtDoor()
        {
            win = true;
            playing = false;
        }

        private int[,] CreateMap()
        {
            class DepthFirstSearch
		{
		    private const int sizeX = 25;
		    private const int sizeY = 25;
		    private int[,] maze = new int[sizeX, sizeY];
		
		
		    public void Generation()
		    {
		        // Initialisation qu'avec des murs
		        for (int i = 0; i < sizeX; i++)
		        {
		            for (int j = 0; j < sizeY; j++)
		            {
		                maze[i, j] = 1;
		            }
		        }
		
		        // Generation en utilisant le parcours en profondeur
		        DepthCourse(1, 1);
		    }
		
		    private void DepthCourse(int x, int y)
		    {
		        maze[y, x] = 0; // Marquage de la cellule actuelle
		
		        List<Tuple<int, int>> neighbor = NewNeighbor(x, y);
		        Random rand = new Random();
		        while (neighbor.Count > 0)
		        {
		            Tuple<int, int> next = neighbor[rand.Next(neighbor.Count)];
		            int nx = next.Item1;
		            int ny = next.Item2;
		            maze[(y + ny) / 2, (x + nx) / 2] = 0; // Suppression du mur entre les 2 sommets
		            DepthCourse(nx, ny);
		            neighbor = NewNeighbor(x, y); // Recherche du nouveau sommet voisin
		        }
		    }
		
		    private List<Tuple<int, int>> NewNeighbor(int x, int y) 
		    {
		        List<Tuple<int, int>> neighbor = new List<Tuple<int, int>>(); // Liste contenant les coordonnées des voisins non visités
		
		        // Vérification des voisins dans les 4 directions à partir du sommet
		        // actuel, et ajout de ceux-ci dans la liste neighbor.
		
		        if (x >= sizeX - 23 && maze[y, x - 2] == 1)
		            neighbor.Add(new Tuple<int, int>(x - 2, y));
		        if (x < sizeX - 2 && maze[y, x + 2] == 1)
		            neighbor.Add(new Tuple<int, int>(x + 2, y));
		        if (y >= sizeY - 23 && maze[y - 2, x] == 1)
		            neighbor.Add(new Tuple<int, int>(x, y - 2));
		        if (y < sizeY -2 && maze[y + 2, x] == 1)
		            neighbor.Add(new Tuple<int, int>(x, y + 2));
		
		        return neighbor;
		    }
		
		    public void Print() //Affichage du labyrinthe
		    {
		        for (int i = 0; i < sizeX; i++)
		        {
		            for (int j = 0; j < sizeY; j++)
		            {
		                if (maze[i, j] == 0)
		                {
		                    Console.Write("  ");
		                }
		                else
		                {
		                    Console.Write("X ");
		                }
		            }
		            Console.WriteLine();
		        }
		    }
		}
        }

        public (int[,], float, int, int) GetInfo()
        {
            return (map.map, player.Health, player.bolts, player.money);
        }

        public float EnemieAtPos(Vector2 pos)
        {
            float damage = 0.0f;
            for (int e = 0; e < enemiesNum && enemiesNum != 0; e++)
                if (enemies[e].pos.x == pos.x && pos.y == enemies[e].pos.y)
                    damage = enemies[e].damage;

            return damage;
        }

        // TODO: Delete debug function 
        public async Task Print()
        {
            int[,] map;
            float health;
            int bolts, money;

            await Task.Run(() =>
            {
                while (playing)
                {
                    (map, health, bolts, money) = GetInfo();

                    Console.WriteLine(health + " HP  /  " + bolts + " Bolts  /  " + money + " Coins");
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

