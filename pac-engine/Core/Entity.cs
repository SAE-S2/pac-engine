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
    }
}
