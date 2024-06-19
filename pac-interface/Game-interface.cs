using pac_engine;
using pac_engine.Core;
using pac_engine.Utils;
using PacDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pac_interface
{
    public partial class Game : Form
    {
        public int tileSize = 48;
        public Panel? pnlGame;
        private PacBot game;
        private PictureBox[,]? grid;
        private PictureBox? PBplayer;
        private PictureBox[]? PBenemy;
        private Entity[]? enemy;
        private Hub hub;
        private int currentLineIndex = 0;
        private DialogueManager dialogueManager;
        private Panel dialoguesPanel;
        private PictureBox boite;
        private PictureBox character;
        private Label dialogueText;
        private int numDialogue;
        private bool isFirstTime;
        private bool dialogueInProgress;
        public Game(Hub hub,PacBot game)
        {
            this.hub = hub;
            InitializeComponent();
            this.game = game;
        }

        private void EndGame(object? sender, GameStateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { EndGame(sender, e); }));
                return;
            }
            Unload();
            if (e.win)
            {
                if (e.level == 10)
                {
                    this.Visible = false;
                    StartDialogue(4, true);
                    Main main = new Main();
                    main.LoadPrincipal();
                }
                else
                {
                    game.StartGame(e.level + 1);
                    game.player.Heal(game.player.regen);
                    game.player.SetActualGame(game.ActualGame);
                    LoadMap();
                    LoadEntities();
                    pnlGame.Visible = true;
                }
            }
            else
            {
                if (!DatabaseManager.GetLevel10Played(Globals.UID,Globals.NumProfil))
                {
                    StartDialogue(1, true);
                    DatabaseManager.SetLevel10Played(Globals.UID, Globals.NumProfil, true);
                }
                this.Visible = false;
                hub.WindowState = FormWindowState.Maximized;
                hub.Show();
            }
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Game_FormClosed(sender, e); }));
                return;
            }
            if (game.ActualGame != null)
            {
                Unload();
            }
        }

        private PictureBox placeWall(Point coords, int level, int type)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(tileSize, tileSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\murs\\Level" + level.ToString() + "\\type" + type.ToString() + ".png")
            };
            return pictureBox;
        }

        public void LoadMap()
        {
            game.ActualGame.GameState += EndGame;
            pnlGame = new Panel();
            pnlGame.SuspendLayout();
            Map map = game.ActualGame.getMap();
            map.CoinEarn += Map_CoinEarn;
            map.DoorOpen += Map_DoorOpen;
            int maxY = map.map.GetLength(0);
            int maxX = map.map.GetLength(1);
            grid = new PictureBox[maxY, maxX];
            while (maxY * tileSize + 4 * tileSize > ClientSize.Height && maxX * tileSize + 4 * tileSize > ClientSize.Width)
            {
                tileSize -= 8;
            }

            for (int line = 0; line < maxY; line++)
            {
                for (int col = 0; col < maxX; col++)
                {
                    if (map.door.x == line && map.door.y == col)
                    {
                        grid[line, col] = new PictureBox()
                        {
                            Location = new Point(col * tileSize, line * tileSize),
                            Size = new Size(tileSize, tileSize),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = Image.FromFile("..\\..\\..\\Resources\\murs\\door.png")
                        };
                    }
                    else if (map.GetWall(line, col))
                    {
                        grid[line, col] = placeWall(new Point(col * tileSize, line * tileSize), game.ActualGame.level, map.GetWallType(line, col)); // LEVEL
                    }
                    else if (map.GetCoin(line, col))
                    {
                        grid[line, col] = new PictureBox()
                        {
                            Location = new Point(col * tileSize, line * tileSize),
                            Size = new Size(tileSize, tileSize),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Coin.png")
                        };

                    }
                    else if (map.GetBolt(line, col))
                    {
                        grid[line, col] = new PictureBox()
                        {
                            Location = new Point(col * tileSize, line * tileSize),
                            Size = new Size(tileSize, tileSize),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png")
                        };
                    }
                    pnlGame.Controls.Add(grid[line, col]);
                }
            }
            pnlGame.AutoSize = true;
            Controls.Add(pnlGame);
            pnlGame.Visible = true;
            pnlGame.Location = new Point( (ClientSize.Width - pnlGame.Width) / 2, (ClientSize.Height - pnlGame.Height) / 2); // Center window
            pnlGame.ResumeLayout();
        }

        public void Unload()
        {
            game.ActualGame.player.StopMovement();
            // Désabonnement aux events
            game.ActualGame.player.PositionChanged -= Player_PositionChanged;

            if (enemy != null)
            {
                foreach (var en in enemy)
                {
                    if (en != null)
                    {
                        en.PositionChanged -= Enemy_PositionChanged;
                    }
                }
            }

            Map map = game.ActualGame.getMap();
            if (map != null)
            {
                map.CoinEarn -= Map_CoinEarn;
                map.DoorOpen -= Map_DoorOpen;
            }

            game.ActualGame.GameState -= EndGame;

            // Dispose PictureBoxes
            if (grid != null)
            {
                foreach (var pictureBox in grid)
                {
                    if (pictureBox != null)
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Dispose();
                    }
                }
            }

            if (PBplayer != null)
            {
                PBplayer.Image?.Dispose();
                PBplayer.Dispose();
                PBplayer = null;
            }

            if (PBenemy != null)
            {
                foreach (var pb in PBenemy)
                {
                    if (pb != null)
                    {
                        pb.Image?.Dispose();
                        pb.Dispose();
                    }
                }
                PBenemy = null;
            }

            if (pnlGame != null)
            {
                Controls.Remove(pnlGame);
                pnlGame.Dispose();
                pnlGame = null;
            }

            grid = null;
            game.player.ResetActualGame();
            //game.player.StopMovement();
            game.ActualGame = null;

            // Forcer le garbage collector pour libérer la mémoire
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        private void Map_DoorOpen(object? sender, DoorOpenEventArgs e)
        {
            grid[e.DoorPos.x, e.DoorPos.y].Image = null;
        }

        private void Map_CoinEarn(object? sender, EarnCoinEventArgs e)
        {
            if (grid[e.Pos.x, e.Pos.y] == null)
                return;

            grid[e.Pos.x, e.Pos.y].Image = null;
        }

        public void LoadEntities()
        {
            enemy = game.ActualGame.GetEnemies();
            Vector2 playerpos = new Vector2(game.ActualGame.map.spawn.y, game.ActualGame.map.spawn.x);
            game.ActualGame.player.PositionChanged += Player_PositionChanged;

            PBplayer = new PictureBox()
            {
                Location = new Point(playerpos.x * tileSize, playerpos.y * tileSize),
                Size = new Size(tileSize, tileSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\Pac-bot1.png")
            };
            pnlGame.Controls.Add(PBplayer);

            PBenemy = new PictureBox[enemy.Length];
            foreach(var en in enemy)
            {
                if (en != null)
                {
                    en.PositionChanged += Enemy_PositionChanged;
                    PBenemy[en.indice] = new PictureBox()
                    {
                        Location = new Point(en.pos.y * tileSize, en.pos.x * tileSize),
                        Size = new Size(tileSize, tileSize),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\BasicEnnemi1-1.png")
                    };
                    pnlGame.Controls.Add(PBenemy[en.indice]);
                    PBenemy[en.indice].BringToFront();
                }
            }
            PBplayer.BringToFront();
        }

        private void Player_PositionChanged(object? sender, PositionChangedEventArgs player)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Player_PositionChanged(sender, player); }));
                return;
            }
            Player_angle(game.ActualGame.player.angle);
            PBplayer.Location = new Point(player.NewPos.y * tileSize, player.NewPos.x * tileSize);
        }

        private void Enemy_PositionChanged(object? sender, PositionChangedEventArgs enemy)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Enemy_PositionChanged(sender, enemy); }));
                return;
            }
            PBenemy[enemy.indice].Location = new Point(enemy.NewPos.y * tileSize, enemy.NewPos.x * tileSize);
        }

        private void Player_angle(int angle)
        {
            Image image = Image.FromFile($"..\\..\\..\\Resources\\Entity\\Pac-bot1.png");
            switch (angle)
            {
                default:
                    break;
                case 0: //Haut
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    PBplayer.Image = image;
                    break;
                case 1: //Droite
                    PBplayer.Image = image;
                    break;
                case 2: //Bas
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    PBplayer.Image = image;
                    break;
                case 3: //Gauche
                    image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    PBplayer.Image = image;
                    break;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.ActualGame != null && game.ActualGame.Playing)
            {
                switch (e.KeyCode)
                {
                    default:
                        break;
                    case Keys.Z:
                        {
                            game.ActualGame.player.AngleChange(0); //Haut
                            break;
                        }
                    case Keys.D:
                        {
                            game.ActualGame.player.AngleChange(1); //Droite
                            break;
                        }
                    case Keys.S:
                        {
                            game.ActualGame.player.AngleChange(2); //Bas
                            break;
                        }
                    case Keys.Q:
                        {
                            game.ActualGame.player.AngleChange(3); //Gauche
                            break;
                        }
                    case Keys.Space:
                        {
                            game.ActualGame.player.AngleChange(4); //Stop
                            break;
                        }
                    case Keys.A:
                    {
                        game.ActualGame.player.ActivePower();
                        break;
                    }
                }
            }
        }
        private void StartDialogue(int numDialogue, bool isFirstTime)
        {
            dialogueInProgress = true; // Indicateur pour savoir si le dialogue est en cours
            currentLineIndex = 0;

            dialogueManager = new DialogueManager(numDialogue, isFirstTime);

            // Initialisation des contrÃ´les pour afficher le dialogue
            dialoguesPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Transparent
            };

            boite = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\Bulle_dialogue.png"),
                Anchor = AnchorStyles.Bottom,
                Cursor = Cursors.Hand
            };

            character = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            dialogueText = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                AutoEllipsis = true,
                Padding = new Padding(10),
                Enabled = true
            };

            boite.Controls.Add(dialogueText);
            Controls.Add(dialoguesPanel);
            dialoguesPanel.Controls.Add(boite);
            dialoguesPanel.Controls.Add(character);
            character.BringToFront();
            dialoguesPanel.BringToFront();

            dialogueText.Click += Dialogue_Click;

            AdjustSizesAndPositions();

            // Afficher la premiÃ¨re ligne de dialogue
            ShowCurrentDialogueLine(numDialogue, isFirstTime);

            // Boucle pour attendre la fin du dialogue
            while (dialogueInProgress)
            {
                Application.DoEvents(); // Permet Ã  l'application de traiter les Ã©vÃ©nements
                // Vous pouvez aussi utiliser Thread.Sleep(100) pour rÃ©duire l'utilisation du CPU
            }

            // Dialogue terminÃ©, nettoyage ou actions aprÃ¨s le dialogue
            Controls.Remove(dialoguesPanel);
            dialoguesPanel.Dispose();
        }

        // MÃ©thode pour afficher la ligne de dialogue courante
        private void ShowCurrentDialogueLine(int numDialogue, bool isFirstTime)
        {
            var currentDialogue = dialogueManager.GetDialogueLine(numDialogue, isFirstTime, currentLineIndex);
            dialogueText.Text = WrapText(currentDialogue.Item1, 48);

            if (currentDialogue.Item2 == "Voix off")
            {
                character.Image = null; // Aucune image pour "Voix off"
            }
            else
            {
                character.Image = Image.FromFile($"..\\..\\..\\Resources\\Entity\\{currentDialogue.Item2}");
            }

            // VÃ©rifier si c'est la derniÃ¨re ligne de dialogue
            if (currentLineIndex >= dialogueManager.GetDialogueLength(numDialogue, isFirstTime))
            {
                dialogueInProgress = false; // Fin du dialogue
            }
        }

        // MÃ©thode appelÃ©e lorsqu'un clic sur le dialogue se produit
        private void Dialogue_Click(object sender, EventArgs e)
        {
            // VÃ©rifier si le dialogue n'est pas terminÃ©
            if (dialogueInProgress && currentLineIndex < dialogueManager.GetDialogueLength(dialogueManager.dialogueIndex, dialogueManager.isFirstTime))
            {
                currentLineIndex++; // Passer Ã  la ligne suivante
                ShowCurrentDialogueLine(dialogueManager.dialogueIndex, dialogueManager.isFirstTime); // Afficher la nouvelle ligne
            }
        }

        // MÃ©thode pour ajuster les tailles et positions des contrÃ´les de dialogue
        private void AdjustSizesAndPositions()
        {
            boite.Size = new Size((int)(dialoguesPanel.ClientSize.Width * 0.8), (int)(dialoguesPanel.ClientSize.Height * 0.5));
            character.Size = new Size((int)(dialoguesPanel.ClientSize.Width * 0.3), (int)(dialoguesPanel.ClientSize.Height * 0.8));

            character.Location = new Point(0, dialoguesPanel.ClientSize.Height - character.Height);
            boite.Location = new Point(dialoguesPanel.ClientSize.Width - boite.Width, dialoguesPanel.ClientSize.Height - boite.Height);

            dialogueText.Size = new Size(boite.Width - 20, boite.Height - 20);
            dialogueText.Location = new Point((boite.Width - dialogueText.Width) / 2, (boite.Height - dialogueText.Height) / 2);
        }

        // MÃ©thode pour dÃ©couper le texte en lignes avec un nombre maximal de caractÃ¨res par ligne
        private string WrapText(string text, int maxCharsPerLine)
        {
            StringBuilder sb = new StringBuilder();
            string[] words = text.Split(' ');
            int currentLineLength = 0;

            foreach (string word in words)
            {
                if (currentLineLength + word.Length + 1 > maxCharsPerLine)
                {
                    sb.Append("\n" + word + " ");
                    currentLineLength = word.Length + 1;
                }
                else
                {
                    sb.Append(word + " ");
                    currentLineLength += word.Length + 1;
                }
            }

            return sb.ToString().Trim();
        }
    }
}
