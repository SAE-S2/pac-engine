namespace pac_engine.Core
{
    public class Guard : Entity
    {
        public Guard()
        {
            this.maxHealth = 1.0f;
            this.Health = 1.0f;
            this.speed = 0.9f;
            this.damage = 1.0f;
        }

        public bool CalledToPos()
        {
            // Call by chief guard
            return false;
        }
    }
}