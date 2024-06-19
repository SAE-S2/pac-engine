namespace pac_engine.Core
{
    public class Invisible : Power
    {
        public Invisible(int levelToSet)
        {
            level = levelToSet;
        }

        public async Task Active(Player player)
        {
            if (!CanActive())
                return;

            cooldown = true;
            player.isInvisible = true;

            await Task.Run(() =>
            {
                switch (level)
                {
                    case 1:
                        Thread.Sleep(15000);
                        break;
                    case 2:
                        Thread.Sleep(17000);
                        break;
                    case 3:
                        Thread.Sleep(20000);
                        break;
                }

                player.isInvisible = false;

                if (level == 3)
                    Thread.Sleep(180000);
                else
                    Thread.Sleep(240000);

                cooldown = false;
            });
        }
    }
}