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
            this.Health -= damage;

            if (this.Health <= 0)
            {
                this.Kill();
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
            this.Health += health;
        }

        public async Task Movement(Map level)
        {
            await Task.Run(() =>
            {
                while (this.Health > 0)
                {
                    switch (this.angle)
                    {
                        case 0: //Z
                            if (level.GetWall(pos.x, pos.y - 1)) { break; }
                            this.pos.y -= 1;
                            break;
                        case 1: //Q
                            if (level.GetWall(pos.x + 1, pos.y)) { break; }
                            this.pos.x += 1;
                            break;
                        case 2: //S
                            if (level.GetWall(pos.x, pos.y + 1)) { break; }
                            this.pos.y += 1;
                            break;
                        case 3: //D
                            if (level.GetWall(pos.x - 1, pos.y)) { break; }
                            this.pos.x -= 1;
                            break;
                    }
                    level.PrintMap(this.pos);
                    //Console.WriteLine($"Position: x={this.pos.x}, y={this.pos.y}");
                    Task.Delay(Globals.ENTITY_SPEED * (int)this.speed).Wait();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            });
        }
        public void AngleChange(int angle)
        {
            if (this.angle < 0 && this.angle > 3)
            {
                return;
            }
            this.angle = angle;
        }
    }
}

