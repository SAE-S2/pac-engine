using pac_engine.Utils;

namespace pac_engine.Core
{
	public class Power
    {
        public int level;
        public int levelMax = 3;
        public event EventHandler<NothingsEventArgs> PowerEnd;
        public bool active = false;

        public bool LevelUp()
        {
            if (level == levelMax)
                return false;

            level++;
            return true;
        }

        public void PowerFinish()
        {
            PowerEnd?.Invoke(this, new NothingsEventArgs { });
        }

        public int GetLevel()
        {
            return level;
        }
    }
}

