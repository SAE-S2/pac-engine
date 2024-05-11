using System.Runtime.Intrinsics.X86;

class DepthFirstSearch
{
    private const int sizeX = 25;
    private const int sizeY = 49;
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

        //Gestion des impasses
        RemoveDeadEnds();
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
        List<Tuple<int, int>> neighbor = new List<Tuple<int, int>>();

        // Vérification des voisins dans les 4 directions à partir du sommet actuel.
        // Nous vérifions si les voisins sont à l'intérieur des limites du tableau.

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

    private void RemoveDeadEnds()
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

    public void Print() // Affichage du labyrinthe
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
