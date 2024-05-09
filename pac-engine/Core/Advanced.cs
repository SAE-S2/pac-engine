using System.Numerics;
using System.Runtime.Intrinsics.X86;

class DepthFirstSearch
{
    private int sizeX;
    private int sizeY;
    private int[,] maze;

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

    public int[,] getMaze()
    {
        return maze;
    }

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

        if (x >= 2 && maze[y, x - 2] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x - 2, y));
        }

        if (x < sizeX - 2 && maze[y, x + 2] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x + 2, y));
        }

        if (y >= 2 && maze[y - 2, x] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x, y - 2));
        }

        if (y < sizeY - 2 && maze[y + 2, x] == 1)
        {
            neighbor.Add(new Tuple<int, int>(x, y + 2));
        }


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
