using pac_engine.Utils;
using pac_engine.Core;
using System.Reflection.Emit;

namespace pac_engine
{
	public class PacBot
	{
		private string title;
		private Vector2 screenSize;
        private Player player;
        public Game ActualGame;

		public PacBot(string title, int x, int y)
		{
			this.title = title;
			screenSize = new Vector2(x, y);
            Console.Title = this.title;
            //Console.SetWindowSize(width: (int)this.screenSize.x, height: (int)this.screenSize.y);
            player = new Player();
        }

        public bool StartGame()
        {
            ActualGame = new Game(ref player);

            return ActualGame.Start();
        }

        static void Main()
        {
            PacBot pacbot = new PacBot(title: "test", x: 500, y: 500);

            bool win = pacbot.StartGame();

            if (win)
            {
                Console.WriteLine("Gagné");
            }
            else
            {
                Console.WriteLine("Perdu");
            }
        }
    }
}