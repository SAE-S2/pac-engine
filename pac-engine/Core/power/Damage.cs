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
            if (!CanActive())
                return;

            cooldown = true;
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

                if (level == 3)
                    Thread.Sleep(180000);
                else
                    Thread.Sleep(240000);

                cooldown = false;
            });
        }
    }
}