using pac_engine.Core;
using pac_engine.Utils;

public class Game
{
    private bool playing = true;
    public bool Playing
    {
        get { return playing; }
    }
    public Map map;
    public Player player;
    public int enemiesCount;
    public int chiefCount;
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
        this.level = level;
        switch (level)
        {
            default:
                enemiesCount = 0;
                chiefCount = 0;
                break;
            case 1:
                enemiesCount = 1;
                chiefCount = 1;
                break;
            case 2:
                enemiesCount = 2;
                chiefCount = 1;
                break;
            case 3:
                enemiesCount = 2;
                chiefCount = 2;
                break;
            case 4:
                enemiesCount = 4;
                chiefCount = 2;
                break;
            case 5:
                enemiesCount = 4;
                chiefCount = 3;
                break;
            case 6:
                enemiesCount = 4;
                chiefCount = 4;
                break;
            case 7:
                enemiesCount = 5;
                chiefCount = 3;
                break;
            case 8:
                enemiesCount = 5;
                chiefCount = 5;
                break;
            case 9:
                enemiesCount = 7;
                chiefCount = 5;
                break;
            case 10:
                enemiesCount = 10;
                chiefCount = 6;
                break;
        }
        int[,] tempMap = CreateMap();
        int k = 0;
        for (int i = 0; i < tempMap.GetLength(0); i++)
            for (int j = 0; j < tempMap.GetLength(1); j++)
            {
                if (tempMap[i, j] == 5)
                {
                    enemies[k] = new Guard(k);
                }
                else if (tempMap[i, j] == 7)
                {
                    enemies[k] = new ChiefGuard(k);
                }
                if (enemies[k] != null)
                {
                    enemies[k].angle = 4;
                    enemies[k].pos = new Vector2(i, j);
                    enemies[k].SetActualGame(this);
                    k++;
                }
            }

        map = new Map(tempMap, this);
        player.pos = map.spawn;
        player.SetActualGame(this);
        player.StartMovement(map);
        
        for (int i = 0; i < enemiesCount; i++)
        {
            _ = enemies[i].Movement(map); // start the movement asynchronously
        }
        return win;
    }

    public void EnemyDie(Entity enemy)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
          if (enemies[i] == enemy)
            {
                enemiesCount--;
                if (i < enemiesCount)
                {
                    for (int j = i; j < enemiesCount; j++)
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
        for (int e = 0; e < enemiesCount && enemiesCount != 0; e++)
            if (enemies[e].pos.x == pos.x && pos.y == enemies[e].pos.y)
                enemy = enemies[e];
        return enemy;
    }

    private int[,] CreateMap()
    {
        DepthFirstSearch map = new DepthFirstSearch(15,25);

        map.Generation();
        for (int i = 0; i < chiefCount; i++)
        {
            map.GenerateChiefGuard();
            
        }
        for (int j = 0; j < enemiesCount-chiefCount; j++)
        {
            map.GenerateGuard();
        }
	      map.RemoveDeadEnds();
	      map.Print();
	      return map.getMaze();
    }

    public Map getMap()
    {
        return map;
    }
}
