﻿namespace pac_engine.Core
{
	public class Shield : Power
	{
        public Shield(int levelToSet)
		{
			level = levelToSet;
        }

        public async Task Active(Player player)
		{
            if (level == 3)
                player.absorption = 1.5f;
            else
                player.absorption = 1.0f;

			await Task.Run(() =>
            {
                if (level == 1)
                    Thread.Sleep(30000);
                else
                    Thread.Sleep(45000);

				player.absorption = 0.0f;
                active = false;
                PowerFinish();
            });
        }
	}
}

