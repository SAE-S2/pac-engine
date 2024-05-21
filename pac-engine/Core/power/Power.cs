namespace pac_engine.Core
{
	public class Power
    {
        public int level;
        public int levelMax = 3;
        public bool cooldown = false;

        public bool CanActive()
        {
            return !cooldown;
        }

        public bool LevelUp()
        {
            if (level == levelMax)
                return false;

            level++;
            return true;
        }
    }
}

