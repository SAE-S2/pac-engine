using System;
using System.Collections.Generic;

class ShortestPath
{
    private int[,] maze;
    private int sizeX;
    private int sizeY;

    public ShortestPath(int[,] maze)
    {
        this.maze = maze;
        sizeX = maze.GetLength(0);
        sizeY = maze.GetLength(1);
    }

    public List<int> FindShortestPath(int startX, int startY, int endX, int endY)
    {
        // Liste des nœuds à explorer
        var openList = new List<Tuple<int, int>>();

        // Liste des nœuds déjà explorés
        var closedList = new HashSet<Tuple<int, int>>();

        // Dictionnaire pour stocker les coûts
        var gCosts = new Dictionary<Tuple<int, int>, int>();

        // Dictionnaire pour stocker les nœuds parents
        var parents = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

        // Ajouter le nœud de départ à la liste ouverte
        openList.Add(new Tuple<int, int>(startX, startY));
        gCosts[new Tuple<int, int>(startX, startY)] = 0;

        // Tant que la liste ouverte n'est pas vide
        while (openList.Count > 0)
        {
            // Trouver le nœud avec le coût le plus bas dans la liste ouverte
            var current = FindLowestCostNode(openList, gCosts);

            // Si le nœud actuel est le nœud de destination, reconstruire le chemin et le renvoyer
            if (current.Item1 == endX && current.Item2 == endY)
            {
                return ReconstructPath(parents, current);
            }

            // Retirer le nœud actuel de la liste ouverte et l'ajouter à la liste fermée
            openList.Remove(current);
            closedList.Add(current);

            // Examiner les voisins du nœud actuel
            foreach (var neighbor in GetNeighbors(current.Item1, current.Item2))
            {
                // Si le voisin est déjà dans la liste fermée, passer à l'itération suivante
                if (closedList.Contains(neighbor))
                    continue;

                // Calculer le coût du voisin
                int tentativeCost = gCosts[current] + 1; // Coût de déplacement uniforme, puisque toutes les arêtes ont le même poids

                // Si le voisin n'est pas dans la liste ouverte ou si ce chemin est meilleur que le précédent, l'ajouter ou le mettre à jour
                if (!openList.Contains(neighbor) || tentativeCost < gCosts[neighbor])
                {
                    parents[neighbor] = current;
                    gCosts[neighbor] = tentativeCost;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        // Aucun chemin trouvé
        return null;
    }

    private Tuple<int, int> FindLowestCostNode(List<Tuple<int, int>> openList, Dictionary<Tuple<int, int>, int> gCosts)
    {
        Tuple<int, int> lowestCostNode = openList[0];
        foreach (var node in openList)
        {
            if (gCosts.ContainsKey(node) && gCosts[node] < gCosts[lowestCostNode])
            {
                lowestCostNode = node;
            }
        }
        return lowestCostNode;
    }

    private List<int> ReconstructPath(Dictionary<Tuple<int, int>, Tuple<int, int>> parents, Tuple<int, int> current)
    {
        var path = new List<int>();
        while (parents.ContainsKey(current))
        {
            var parent = parents[current];
            if (parent.Item1 < current.Item1)
                path.Add(2); // Bas
            else if (parent.Item1 > current.Item1)
                path.Add(0); // Haut
            else if (parent.Item2 < current.Item2)
                path.Add(3); // Droite
            else
                path.Add(1); // Gauche
            current = parent;
        }
        path.Reverse(); // Inverser le chemin pour qu'il soit du début à la fin
        return path;
    }

    private List<Tuple<int, int>> GetNeighbors(int x, int y)
    {
        var neighbors = new List<Tuple<int, int>>();

        // Ajouter les voisins qui sont des cases vides
        if (x > 0 && (maze[x - 1, y] == 0 || maze[x - 1, y] == 2 || maze[x - 1, y] == 3))
            neighbors.Add(new Tuple<int, int>(x - 1, y));
        if (x < sizeX - 1 && (maze[x + 1, y] == 0 || maze[x + 1, y] == 2 || maze[x + 1, y] == 3))
            neighbors.Add(new Tuple<int, int>(x + 1, y));
        if (y > 0 && (maze[x, y - 1] == 0 || maze[x, y - 1] == 2 || maze[x, y - 1] == 3))
            neighbors.Add(new Tuple<int, int>(x, y - 1));
        if (y < sizeY - 1 && (maze[x, y + 1] == 0 || maze[x, y + 1] == 2 || maze[x, y + 1] == 3))
            neighbors.Add(new Tuple<int, int>(x, y + 1));

        return neighbors;
    }
}
