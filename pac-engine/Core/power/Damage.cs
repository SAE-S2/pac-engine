using System;
using pac_engine.Utils;

namespace pac_engine.Core
{
    public class Damage : Power
    {
        public Damage(int levelToSet)
        {
            level = levelToSet;
        }

        public async Task Active(Player player)
        {
            active = true;
            player.imortal = true;
            player.damage = 10f;

            await Task.Run(() =>
            {
                switch (level)
                {
                    case 1:
                        Thread.Sleep(10000);
                        break;
                    case 2:
                        Thread.Sleep(12000);
                        break;
                    case 3:
                        Thread.Sleep(15000);
                        break;
                }

                player.imortal = false;
                player.damage = 0f;
                active = false;
                PowerFinish();
            });
        }
    }
}