using System.Runtime.Intrinsics.X86;

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
