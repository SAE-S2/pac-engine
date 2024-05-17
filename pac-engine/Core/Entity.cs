using pac_engine.Utils;

namespace pac_engine.Core
{
    public class Entity
    {
        private float health;
        public float Health
        {
            get { return health; }
            set { health = (value <= maxHealth) ? value : maxHealth; }
        }

        public float maxHealth;
        public float speed;
        public float damage;
        public bool imortal = false;
        public int angle = 0;
        public Vector2 pos = new Vector2();
        public Game? actualGame;

        /// <summary>
        /// Entity take specified amount of <paramref name="damage"/>.
        /// <para></para>
        /// (<see cref="Kill"/> player if health below or equal 0)
        /// </summary>
        /// <returns>Damages maked</returns>
        public bool TakeDamage(float damage)
        {
            if (imortal)
                return false;

            Health -= damage;

            if (Health <= 0.1f)
                Kill();

            return true;
        }

        public bool Kill()
        {
            if (imortal)
                return false;
            
            return true;
        }

        public void Heal(float health)
        {
            Health += health;
        }

        public async Task Movement(Map level)
        {
            bool posChange;
            int nbDeplacment = 10;
            ShortestPath shortestPath = new ShortestPath(actualGame.map.map);
            List<int> chemin = new List<int>();
            await Task.Run(() =>
            {
                while (Health > 0)
                {
                    // Contient les angles du chemin
                    chemin = shortestPath.FindShortestPath(pos, actualGame.player.pos);

                    if (chemin == null || chemin.Count == 0)
                        continue;

                    posChange = false;
                    AngleChange(chemin[0]);
                    chemin.RemoveAt(0);
                    switch (angle)
                    {
                        case 0: //Z (Haut)
                            if (level.GetWall((int)pos.x - 1, (int)pos.y)) { break; }
                            pos.x -= 1;
                            posChange = true;
                            break;
                        case 1: //Q (Gauche)
                            if (level.GetWall((int)pos.x, (int)pos.y + 1)) { break; }
                            pos.y += 1;
                            posChange = true;
                            break;
                        case 2: //S (Bas)
                            if (level.GetWall((int)pos.x + 1, (int)pos.y)) { break; }
                            pos.x += 1;
                            posChange = true;
                            break;
                        case 3: //D (Droite)
                            if (level.GetWall((int)pos.x, (int)pos.y - 1)) { break; }
                            pos.y -= 1;
                            posChange = true;
                            break;
                        case 4: //STOP
                            break;
                    }

                    if (posChange && actualGame.player.pos.x == pos.x && actualGame.player.pos.y == pos.y)
                        actualGame.player.TakeDamage(damage);

                    nbDeplacment++;

                    Task.Delay((int)(Globals.ENTITY_SPEED * speed)).Wait();
                }
            });
        }


        public void AngleChange(int angle)
        {
            //0:Haut 1:Gauche 2:Bas 3:Droite 4:Aucun
            if (angle < 0 && angle > 4)
            {
                return;
            }
            this.angle = angle;
        }

        public void SetActualGame(Game game)
        {
            actualGame = game;
        }

        public void ResetActualGame()
        {
            actualGame = null;
        }
    }
}