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
                active = false;
                PowerFinish();
            });
        }
    }
}