using pac_engine.Core;
using pac_engine.Utils;

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
    public int level;
    private bool win = false;
    public event EventHandler<GameStateEventArgs>? GameState;

    public Game(ref Player playerRef)
    {
        player = playerRef;
    }

    public Entity[] GetEnemies()
    {
        return enemies;
    }

    public bool Start(int level)
    {
        int[,] tempMap = CreateMap();
        for (int i = 0; i < tempMap.GetLength(0); i++)
        {
            for (int j = 0; j < tempMap.GetLength(1); j++)
            {
                if (tempMap[i, j] == 5)
                {
                    enemies[enemiesNum] = new Guard(enemiesNum);
                    enemies[enemiesNum].angle = 3;
                    enemies[enemiesNum].pos = new Vector2(i, j);
                    enemies[enemiesNum].SetActualGame(this);
                    enemiesNum++;
                }
                else if (input == ConsoleKey.A)
                {
                    player.ActivePower();
                }
            }
        }

        map = new Map(tempMap, this);
        this.level = level;
        player.pos = map.spawn;
        player.SetActualGame(this);
        player.StartMovement(map);
        for (int i = 0; i < enemiesNum; i++)
        {
            _ = enemies[i].Movement(map); // start the movement asynchronously
        }
        return win;
    }

    public void EnemyDie(Entity enemy)
    {
        for (int i = 0; i < enemiesNum; i++)
        {
            if (enemies[i] == enemy)
            {
                enemiesNum--;
                if (i < enemiesNum)
                {
                    for (int j = i; j < enemiesNum; j++)
                    {
                        enemies[j] = enemies[j+1];
                    }
                }
            }
        }
    }

    public void PlayerDied()
    {
        win = false;
        playing = false;
        GameState?.Invoke(this, new GameStateEventArgs { win = false });
    }

    public void PlayerAtDoor()
    {
        win = true;
        playing = false;
        GameState?.Invoke(this, new GameStateEventArgs { win = true, level = level });
    }


    public (int[,], float, float, int, int) GetInfo()
    {
        return (map.map, player.Health, player.absorption, player.bolts, player.money);
    }

    public Entity EnemyAtPos(Vector2 pos)
    {
        Entity enemy = player;
        for (int e = 0; e < enemiesNum && enemiesNum != 0; e++)
            if (enemies[e].pos.x == pos.x && pos.y == enemies[e].pos.y)
                enemy = enemies[e];
        return enemy;
    }

    private int[,] CreateMap()
    {
        // TODO: Implement algo
        int[,] map = {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 5, 0, 1 },
            { 1, 0, 0, 1, 0, 1, 1, 0, 1 },
            { 1, 1, 0, 3, 0, 0, 0, 5, 1 },
            { 1, 0, 0, 1, 0, 1, 1, 0, 1 },
            { 1, 1, 1, 1, 2, 1, 1, 1, 1 }
        };

        return map;
    }

    public Map getMap()
    {
        return map;
    }

    public (int[,], float, int, int) GetInfo()
    {
        return (map.map, player.Health, player.bolts, player.money);
    }
}
