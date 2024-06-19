using pac_engine.Utils;
using pac_engine.Core;
using System.Net.Security;

namespace pac_engine
{
	public class PacBot
	{
        private string title;
		private Vector2 screenSize;
        public Player player;
        public Game ActualGame;
        public string name;
        public int price;


        public PacBot(string title, int x, int y)
		{
			this.title = title;
			screenSize = new Vector2(x, y);
            //Console.Title = this.title;
            //Console.SetWindowSize(width: (int)this.screenSize.x, height: (int)this.screenSize.y);
        }

        public bool StartGame(int level)
        {
            ActualGame = new Game(ref player);
            return ActualGame.Start(level);
        }

        public string[] GetProfils()
        {
            string[] profils = { "Profil 1", "Profil 2" };
            return profils;
        }

        public void LoadWithProfil(int profil)
        {
            // add load from db
            name = "profil " + profil;
            player = new Player();
        }

        public void initializeGame(int level)
        {
            price = 0;
            bool win = StartGame(level);
        }
    }
}