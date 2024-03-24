namespace pac_engine.Core
{
    public class ChiefGuard : Entity
    {
        public ChiefGuard()
        {
            this.maxHealth = 1.0f;
            this.Health = 1.0f;
            this.speed = 0.7f;
            this.damage = 0.5f;
        }

        public bool CallToPos()
        {
            // Call other guard
            return false;
        }
    }
}