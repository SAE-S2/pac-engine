using pac_engine.Utils;
using pac_engine.Core;
using System.Reflection.Emit;

namespace pac_engine
{
	public class Game
	{
		private string title;
		private Vector2 screenSize;
        private Player player;

		public Game(string title, int x, int y)
		{
			this.title = title;
			this.screenSize = new Vector2(x, y);
            Console.Title = this.title;
            //Console.SetWindowSize(width: (int)this.screenSize.x, height: (int)this.screenSize.y);
            player = new Player();
        }

        static int[,] TestMap()
        {
            int[,] map = {
                {
                    1, 1, 1, 1, 1, 1, 1, 1, 1
                },
                {
                    1, 0, 0, 0, 0, 0, 5, 0, 1
                },
                {
                    1, 0, 0, 1, 0, 1, 1, 0, 1
                },
                {
                    1, 1, 0, 3, 0, 0, 0, 0, 1
                },
                {
                    1, 0, 0, 1, 0, 1, 1, 0, 1
                },
                {
                    1, 1, 1, 1, 2, 1, 1, 1, 1
                }
            };
            return map;
        }

        static void Main()
        {
            Game pacbot = new Game(title: "test", x: 500, y: 500);
            Console.WriteLine("Creation du joueur");
            Console.WriteLine("Déplacement du joueur:");
            Vector2 lastPos = new Vector2();
            Console.WriteLine($"Position de départ: x={pacbot.player.pos.x}, y={pacbot.player.pos.y}");
            Vector2 test = new Vector2(0, 0);
            pacbot.player.pos += test;
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

            int[,] map = TestMap();
            Map level1 = new Map(map, ref pacbot.player);
            pacbot.player.pos = level1.spawn;
            pacbot.player.Movement(level1);
            level1.Print();

            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                 
                if (input == ConsoleKey.Z)
                {
                    pacbot.player.AngleChange(0);
                }
                if (input == ConsoleKey.Q)
                {
                    pacbot.player.AngleChange(3);
                }
                if (input == ConsoleKey.S)
                {
                    pacbot.player.AngleChange(2);
                }
                if (input == ConsoleKey.D)
                {
                    pacbot.player.AngleChange(1);
                }
                if (input == ConsoleKey.Spacebar)
                {
                    pacbot.player.AngleChange(4);
                }
            }
        }
    }
}