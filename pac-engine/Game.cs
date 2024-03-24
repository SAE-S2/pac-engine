using pac_engine.Utils;
using pac_engine.Core;

namespace pac_engine
{
	public class Game
	{
		private string title;
		private Vector2 screenSize;
        private Player player;

		public Game(string title, float x, float y)
		{
			this.title = title;
			this.screenSize = new Vector2(x, y);
            Console.Title = this.title;
            //Console.SetWindowSize(width: (int)this.screenSize.x, height: (int)this.screenSize.y);
            player = new Player();
        }

        static void Main()
        {
            Game pacbot = new Game(title: "test", x: 500, y: 500);
            Console.WriteLine("Creation du joueur");
            Console.WriteLine("Déplacement du joueur:");
            Vector2 lastPos = new Vector2();
            Console.WriteLine($"Position de départ: x={pacbot.player.pos.x}, y={pacbot.player.pos.y}");
            Vector2 test = new Vector2(5.0f, 2.0f);
            pacbot.player.pos.Add(test);
            Console.WriteLine($"Position de fin: x={pacbot.player.pos.x}, y={pacbot.player.pos.y}");
            Console.WriteLine($"Distance: {pacbot.player.pos.Distance(lastPos)}");
            Console.WriteLine($"Pacbot récupère 3 pièces ({pacbot.player.money})");
            pacbot.player.money += 3;
            Console.WriteLine("Un ennemi apparait");
            Entity enemy = new Guard();
            Console.WriteLine("Un autre ennemi apparait");
            Entity enemy2 = new ChiefGuard();
            Console.WriteLine($"Vie: {pacbot.player.Health}");
            pacbot.player.TakeDamage(enemy.damage);
            Console.WriteLine($"Vie: {pacbot.player.Health}");
            Console.WriteLine("Activation bouclier");
            pacbot.player.imortal = true;
            pacbot.player.TakeDamage(enemy.damage);
            Console.WriteLine($"Vie: {pacbot.player.Health}");
            pacbot.player.imortal = false;
            Console.WriteLine("Desactivation bouclier");
            pacbot.player.TakeDamage(enemy2.damage);
            Console.WriteLine($"Vie: {pacbot.player.Health}");
            Console.WriteLine("Appueyer sur entrer pour quitter");
            Console.WriteLine("Appueyer sur ZQSD pour ce déplacer");
            float max = 9.9f;
            Vector2[] testCoin = new Vector2[100];
            testCoin[0] = new Vector2(pacbot.player.pos.x, pacbot.player.pos.y);
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                 
                if (input == ConsoleKey.Z)
                {
                    if (pacbot.player.pos.x <= max)
                        pacbot.player.pos.Add(new Vector2(0.5f, 0.0f));
                }
                if (input == ConsoleKey.Q)
                {
                    if (pacbot.player.pos.y <= max)
                        pacbot.player.pos.Add(new Vector2(0.0f, 0.5f));
                }
                if (input == ConsoleKey.S)
                {
                    if (pacbot.player.pos.x >= -max)
                        pacbot.player.pos.Add(new Vector2(-0.5f, 0.0f));
                }
                if (input == ConsoleKey.D)
                {
                    if (pacbot.player.pos.y >= -max)
                        pacbot.player.pos.Add(new Vector2(0.0f, -0.5f));
                }
                if ((pacbot.player.pos.x % 1 == 0) && (pacbot.player.pos.y % 1 == 0))
                {
                    bool exist = false;
                    int i = 0;
                    for (i = 0; testCoin[i] != null && exist == false && i < 100; i++)
                    {
                        if (pacbot.player.pos.Equals(testCoin[i]))
                        {
                            exist = true;
                        }
                    }
                    if (exist == false)
                    {
                        testCoin[i] = new Vector2(pacbot.player.pos.x, pacbot.player.pos.y);
                        pacbot.player.money += 1;
                        Console.WriteLine($"Récupération de 1 pièce: pièces={pacbot.player.money}");
                    }
                }
                Console.WriteLine($"Position: x={pacbot.player.pos.x}, y={pacbot.player.pos.y}");
                Console.WriteLine($"Distance 0,0: {pacbot.player.pos.Distance(lastPos)}");
            }
        }
    }
}