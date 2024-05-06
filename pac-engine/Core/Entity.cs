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
            Random random = new Random();
            int i = 0; // DEV VAR
            int iToGo = random.Next(1, 10); ; // DEV VAR
            await Task.Run(() =>
            {
                while (Health > 0)
                {
                    posChange = false;
                    switch (angle)
                    {
                        case 0: //Z (Haut)
                            if (level.GetWall(pos.x - 1, pos.y)) { break; }
                            pos.x -= 1;
                            posChange = true;
                            break;
                        case 1: //Q (Gauche)
                            if (level.GetWall(pos.x, pos.y + 1)) { break; }
                            pos.y += 1;
                            posChange = true;
                            break;
                        case 2: //S (Bas)
                            if (level.GetWall(pos.x + 1, pos.y)) { break; }
                            pos.x += 1;
                            posChange = true;
                            break;
                        case 3: //D (Droite)
                            if (level.GetWall(pos.x, pos.y - 1)) { break; }
                            pos.y -= 1;
                            posChange = true;
                            break;
                        case 4: //STOP
                            break;
                    }

                    if (posChange && actualGame.player.pos.x == pos.x && actualGame.player.pos.y == pos.y)
                        actualGame.player.TakeDamage(damage);

                    Task.Delay((int)(Globals.ENTITY_SPEED * speed)).Wait();

                    if (i == iToGo)
                    {
                        i = 0;
                        angle = random.Next(0, 3);
                        iToGo = random.Next(1, 10);
                    }
                    i++;
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