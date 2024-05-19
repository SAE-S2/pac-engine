using pac_engine.Utils;
using System.ComponentModel;
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
        public int level;
        private bool win = false;
        public event EventHandler<GameStateEventArgs> GameState;

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
            this.level = level;
            player.pos = map.spawn;
            player.SetActualGame(this);
            player.StartMovement(map);

            for (int i = 0; i < enemiesNum; i++)
                enemies[i].Movement(map);
            return win;
        }

        public void PlayerDied()
        {
            GameState?.Invoke(this, new GameStateEventArgs { win = false });
            playing = false;
        }

        public void PlayerAtDoor()
        {
            GameState?.Invoke(this, new GameStateEventArgs { win = true, level = level });
            win = true;
            playing = false;
        }

        private int[,] CreateMap()
        {
            // TODO: Implement algo

            int[,] map = {
                {
                    1, 1, 1, 1, 1, 1, 1, 1, 1
                },
                {
                    1, 0, 0, 0, 0, 0, 5, 0, 1
                },
                {
                    1, 0, 0, 1, 0, 1, 1, 0, 1
                },
                {
                    1, 1, 0, 3, 0, 0, 0, 0, 1
                },
                {
                    1, 0, 0, 1, 0, 1, 1, 0, 1
                },
                {
                    1, 1, 1, 1, 2, 1, 1, 1, 1
                }
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

        public float EnemieAtPos(Vector2 pos)
        {
            float damage = 0.0f;
            for (int e = 0; e < enemiesNum && enemiesNum != 0; e++)
                if (enemies[e].pos.x == pos.x && pos.y == enemies[e].pos.y)
                    damage = enemies[e].damage;

            return damage;
        }

        
    }
}

