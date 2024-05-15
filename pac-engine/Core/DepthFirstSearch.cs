using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System;

class Enemy
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public Enemy(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }
}
class DepthFirstSearch
{
    private int sizeX;
    private int sizeY;
    private int[,] maze;
    private List<Enemy> enemies = new List<Enemy>(); // Stock des ennemis

    public DepthFirstSearch(int sizeX, int sizeY)
    {
        if (sizeY % 2 == 0 || sizeX % 2 == 0)
        {
            throw new ArgumentException("Les bornes doivent etre impaires");
        }
        else
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            maze = new int[sizeX, sizeY];
        }
    }

    public DepthFirstSearch(Vector2 size)
    {
        if (sizeY % 2 == 0 || sizeX % 2 == 0)
        {
            throw new ArgumentException("Les bornes doivent etre impaires");
        }
        else
        {
            this.sizeX = (int)size.X;
            this.sizeY = (int)size.Y;
            maze = new int[sizeX, sizeY];
        }
    }

    public int[,] GetMaze()
    {
        return maze;
    }

    public void Generation()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                maze[i, j] = 1;
            }
        }

        // Generation en utilisant le parcours en profondeur
        DepthCourse(1, 1);
        GeneratePlayer();

        int playerX = -1;
        int playerY = -1;

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (GetMaze()[i, j] == 2)
                {
                    playerX = j;
                    playerY = i;
                    break;
                }
            }
            if (playerX != -1 && playerY != -1)
                break;
        }

        if (playerX == -1 || playerY == -1)
        {
            throw new InvalidOperationException("Impossible de trouver les coordonnées du joueur.");
        }
        else
        {
            // Générer les ennemis en passant les coordonnées du joueur en paramètres
            GenerateEnemies(playerX, playerY);
        }
    }

    public void AddExit()
    {
        int exitX;
        int exitY;

        exitX = sizeX - 1;
        exitY = sizeY / 2 + 1;

        maze[exitY, exitX] = 0; // Placer la sortie
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

        // Vérification des voisins dans les 4 directions à partir du sommet
        // actuel, et ajout de ceux-ci dans la liste neighbor.

        if (x >= 2 && maze[y, x - 2] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x - 2, y));
        }

        if (x < maze.GetLength(1) - 2 && maze[y, x + 2] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x + 2, y));
        }

        if (y >= 2 && maze[y - 2, x] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x, y - 2));
        }

        if (y < maze.GetLength(0) - 2 && maze[y + 2, x] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x, y + 2));
        }

        return neighbor;
    }

    public void RemoveDeadEnds()
    {
        Random rand = new Random();

        for (int i = 1; i < sizeX - 1; i += 2) // On parcourt tout le labyrinthe hors bordure
        {
            for (int j = 1; j < sizeY - 1; j += 2)
            {
                if (maze[i, j] == 0)
                {
                    int countWalls = 0; // Réinitialisation du nombres de murs

                    if (maze[i - 1, j] == 1)
                    {
                        countWalls++;
                    }
                    if (maze[i + 1, j] == 1)
                    {
                        countWalls++;
                    }
                    if (maze[i, j - 1] == 1)
                    {
                        countWalls++;
                    }
                    if (maze[i, j + 1] == 1)
                    {
                        countWalls++;
                    }

                    if (countWalls == 3)
                    {
                        List<Tuple<int, int>> options = new List<Tuple<int, int>>();

                        if (maze[i - 1, j] == 1 && i - 1 != 0)
                        {
                            options.Add(new Tuple<int, int>(i - 1, j)); // Sans compter les bordures
                        }
                        if (maze[i + 1, j] == 1 && i + 1 != sizeX - 1)
                        {
                            options.Add(new Tuple<int, int>(i + 1, j));
                        }
                        if (maze[i, j - 1] == 1 && j - 1 != 0)
                        {
                            options.Add(new Tuple<int, int>(i, j - 1));
                        }
                        if (maze[i, j + 1] == 1 && j + 1 != sizeY - 1)
                        {
                            options.Add(new Tuple<int, int>(i, j + 1));
                        }

                        int changeIndex = rand.Next(options.Count);
                        Tuple<int, int> changeCell = options[changeIndex];
                        maze[changeCell.Item1, changeCell.Item2] = 0; // Remplacement aléatoire d'un seul mur par
                                                                      // une case vide.                                
                    }
                }
            }
        }
    }

    public void GeneratePlayer()
    {
        Random rand = new Random();
        int playerX;
        int playerY;
        int side = rand.Next(4); // Choisir aléatoirement un bord (0: haut, 1: droite, 2: bas, 3: gauche)

        switch (side)
        {
            case 0: // Haut
                playerX = rand.Next(1, sizeX - 1); // Éviter les coins
                playerY = 1;
                break;
            case 1: // Droite
                playerX = sizeX - 2;
                playerY = rand.Next(1, sizeY - 1);
                break;
            case 2: // Bas
                playerX = rand.Next(1, sizeX - 1);
                playerY = sizeY - 2;
                break;
            case 3: // Gauche
                playerX = 1;
                playerY = rand.Next(1, sizeY - 1);
                break;
            default:
                throw new InvalidOperationException("Côté invalide");
        }

        maze[playerY, playerX] = 2; // 2 représente le joueur dans la matrice
    }

    public void GenerateEnemies(int playerX, int playerY)
    {
        Random rand = new Random();
        int enemyCount = 0;

        while (enemyCount < 3) // Limite le nombre d'ennemis à 3
        {
            int posX = rand.Next(1, sizeX - 1);
            int posY = rand.Next(1, sizeY - 1);

            // Vérifie que la position n'est pas un mur ni à l'extérieur des limites du labyrinthe
            if (maze[posY, posX] == 0 && Distance(playerX, playerY, posX, posY) >= 5)
            {
                enemies.Add(new Enemy(posX, posY));
                enemyCount++;
            }
        }
    }

    private double Distance(int x1, int y1, int x2, int y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    private bool EnemyLocation(int x, int y)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.PositionX == x && enemy.PositionY == y)
            {
                return true;
            }
        }
        return false;
    }

    public void Print()
    {
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (EnemyLocation(j, i) && maze[i, j] == 0)
                {
                    Console.Write("0 "); // Si un ennemi est présent à cette position dans le labyrinthe, affiche "0".
                }
                else if (maze[i, j] == 0)
                {
                    Console.Write("  "); // Si la case est vide, afficher deux espaces pour représenter un chemin libre
                }
                else if (maze[i, j] == 2)
                {
                    Console.Write("P "); // Si la case contient le joueur, afficher "P" pour le représenter
                }
                else
                {
                    Console.Write("X "); // Si la case n'est pas vide et n'est pas un ennemi, afficher "X" pour représenter un mur
                }
            }
            Console.WriteLine();
        }
    }
}
