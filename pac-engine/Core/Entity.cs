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

        public bool TakeDamage(float damage)
        {
            if (imortal)
            {
                return false;
            }
            Health -= damage;

            if (Health <= 0)
            {
                Kill();
            }
            return true;
        }

        public bool Kill()
        {
            if (imortal)
            {
                return false;
            }
            // DO KILL THINGS
            return true;
        }

        public void Heal(float health)
        {
            Health += health;
        }

        public async Task Movement(Map level)
        {
            await Task.Run(() =>
            {
                while (Health > 0)
                {
                    switch (angle)
                    {
                        case 0: //Z (Haut)
                            if (level.GetWall(pos.x - 1, pos.y)) { break; }
                            pos.x -= 1;
                            break;
                        case 1: //Q (Gauche)
                            if (level.GetWall(pos.x, pos.y + 1)) { break; }
                            pos.y += 1;
                            break;
                        case 2: //S (Bas)
                            if (level.GetWall(pos.x + 1, pos.y)) { break; }
                            pos.x += 1;
                            break;
                        case 3: //D (Droite)
                            if (level.GetWall(pos.x, pos.y - 1)) { break; }
                            pos.y -= 1;
                            break;
                        case 4: //STOP
                            break;
                    }
                    //Console.WriteLine($"Position {this}: x={pos.x}, y={pos.y}");
                    Task.Delay((int)(Globals.ENTITY_SPEED * speed)).Wait();
                    
                }
            });
        }
        public void AngleChange(int angle)
        {
            //0:Haut 1:Gauche 2:Bas 3:Droite
            if (angle < 0 && angle > 3)
            {
                return;
            }
            this.angle = angle;
        }
    }
}