int sizeX = 25;
int sizeY = 25;
int[,] maze = new int[sizeX, sizeY]; // Choix de la structure utilisée : matrice 25x25.
Random random = new Random();
float purcentageWall;
float totalWall = 0;
float purcentageVoid;
float totalVoid = 0;

for (int i = 0; i < sizeX; i++)
{
    for (int j = 0; j < sizeY; j++)
    {
        if (i == 0 || j == 0 || i == sizeX - 1 || j == sizeY - 1)
        {
            maze[i, j] = 1;
        }
        else
        {
            maze[i, j] = random.Next(2); // La case de la matrice de coordonnées (i, j)
                                         // prend une valeur aléatoire entre 0 inclus et 2 exclu
            if (maze[i, j] == 1) 
            {
                totalWall += 1;
            }
            else
            {
                totalVoid += 1;
            }
        }

        if (maze[i, j] == 1)
        {
            Console.Write("X ");
        }
        else
        {
            Console.Write("  ");
        }
    }
    Console.WriteLine(); // Saut de ligne
}

Console.WriteLine();

purcentageVoid = totalVoid * 100 / ((sizeX - 2) * (sizeY - 2)); // Pourcentage de portes.
purcentageWall = totalWall * 100 / ((sizeX - 2) * (sizeY - 2)); // Pourcentage de murs.

Console.WriteLine($"Le pourcentage de portes est de {purcentageVoid} %");
Console.WriteLine($"Le pourcentage de mur est de {purcentageWall} %");

