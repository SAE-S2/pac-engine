
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
        public Game scoreCount;
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

        public void initializeGame()
        {
            name = "profil 1";
            player = new Player();
            price = 0;
            bool win = StartGame(1);
        }

        static void Main()
        {
            PacBot pacbot = new PacBot(title: "test", x: 500, y: 500);


            string[] profils = pacbot.GetProfils(); // Récupération des profils

            bool createProfil = false;
            if (profils.Length < 1)
            {
                createProfil = true;
            }
            else if (profils.Length < 3)
            {
                Console.WriteLine("Créer un nouveau profil (O/N)");
                createProfil = Console.ReadKey().Key == ConsoleKey.O;
            }

            if (createProfil)
            {
                // creation profil
                Console.WriteLine("Entrer pseudo: ");
                pacbot.name = Console.ReadLine();

                // TODO: Insert en bdd

                // Level de debut
            }
            else
            {
                // Choix profil
                int selected = -1;
                while (selected < 0 || profils.Length < selected)
                {
                    Console.WriteLine("Choisir un profil:");
                    for (int i = 0; i < profils.Length; i++)
                    {
                        Console.WriteLine("[" + (i + 1) + "]: " + profils[i]);
                    };

                    ConsoleKey input = Console.ReadKey().Key;
                    if (input == ConsoleKey.D1)
                    {
                        selected = 0;
                    }
                    else if (input == ConsoleKey.D2)
                    {
                        selected = 1;
                    }
                    else if (input == ConsoleKey.D3)
                    {
                        selected = 2;
                    }
                }
                // Récupération des données du joueur
                pacbot.name = profils[selected];
                //...
                // Chargement de toutes les données relative au profil
            }

            // Creation du joueur en fonction des stats
            pacbot.player = new Player();

            // Chargement du prix actuel du jeu
            pacbot.price = 0;

            bool lose = false;
            pacbot.ActualGame.level = 1;
            while (pacbot.ActualGame.level < 11)
            {
                if (pacbot.ActualGame.level == 1)
                {
                    Console.WriteLine("Argent: " + pacbot.player.money);
                    Console.WriteLine("Prix de sortie: " + pacbot.price);
                    Thread.Sleep(2000);
                    if (pacbot.player.money < pacbot.price)
                    {
                        lose = true;
                        pacbot.ActualGame.level = 11;
                    }
                    else
                    {
                        pacbot.player.money -= pacbot.price;
                    }
                }

                if (!lose)
                {
                    bool win = pacbot.StartGame(1);

                    if (win)
                    {
                        pacbot.ActualGame.level++;
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Passage au prochain niveau (" + pacbot.ActualGame.level + ")");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        pacbot.ActualGame.level = 1;
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Vous êtes mort");
                        Console.WriteLine("Vous revenez en prison");
                        pacbot.player.Heal(pacbot.player.maxHealth - pacbot.player.Health);
                        if (pacbot.price == 0)
                        {
                            pacbot.price += 10;
                        }
                        pacbot.price += (int)Math.Floor(pacbot.price * 0.2);
                        Thread.Sleep(2000);
                    }
                }
            }

            if (lose)
            {
                Console.WriteLine("Vous n'avez plus assez d'argent.");
                Console.WriteLine("Vous avez perdu.");
                // Supprimer le profil ?
            }
            else
            {
                Console.WriteLine("Vous avez fini le jeu");
            }

        }
    }
}
