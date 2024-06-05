using pac_engine;
using pac_engine.Core;
using pac_engine.Utils;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pac_interface
{
    public partial class Game : Form
    {
        public int tileSize = 256;
        public Panel? pnlGame;
        private PacBot game;
        private PictureBox[,]? grid;
        private PictureBox? PBplayer;
        private PictureBox[]? PBenemy;
        private Entity[]? enemy;
        private int coinCount = 0;
        private Label coinCounterLabel;
        private int boltCount = 0;
        private Label boltCounterLabel;
        private Label levelLabel;
        private int levelCount;
        private Label scoreLabel;
        private int scoreCount;
        private Label powerLabel;
        private Panel hearthPanel;
        private PictureBox? shieldPictureBox;
        private PictureBox? usedShieldPictureBox;
        private PictureBox? invisiblePictureBox;
        private PictureBox? usedInvisiblePictureBox;
        private PictureBox? damagePictureBox;
        private PictureBox? usedDamagePictureBox;

        public Game(PacBot game)
        {
            InitializeComponent();
            this.game = game;

            InitializeElements();
        }

        private PictureBox Coin(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(30, 30),
                Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Coin.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox Hearth(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(30, 30),
                Image = Image.FromFile("..\\..\\..\\Resources\\Coeur\\Coeur1.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox Bolt(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(30, 30),
                Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox Shield(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(50, 50),
                Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\Bouclier.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox ShieldUsed(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(50, 50),
                Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\Bouclier-used.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox Invisible(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(50, 50),
                Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\Invisible.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox InvisibleUsed(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(50, 50),
                Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\Invisible-used.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox Damage(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(50, 50),
                Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\Degats.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox DamageUsed(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(50, 50),
                Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\Degats-used.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private void InitializeElements()
        {
            levelCount = 1;

            switch (game.player.selectedPower)
            {
                case 1:
                    shieldPictureBox = Shield(new Point(1230, 5));
                    Controls.Add(shieldPictureBox);
                    break;
                case 2:
                    damagePictureBox = Damage(new Point(1230, 5));
                    Controls.Add(damagePictureBox);
                    break;
                case 3:
                    invisiblePictureBox = Invisible(new Point(1230, 5));
                    Controls.Add(invisiblePictureBox);
                    break;
            }

            PictureBox coin = Coin(new Point(1555, 10));
            Controls.Add(coin);

            PictureBox bolt = Bolt(new Point(1490, 10));
            Controls.Add(bolt);

            coinCounterLabel = new Label()
            {
                Location = new Point(1527, 10),
                Size = new Size(50, 30),
                Text = $"{coinCount}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(coinCounterLabel);

            boltCounterLabel = new Label()
            {
                Location = new Point(1462, 10),
                Size = new Size(50, 30),
                Text = "0",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(boltCounterLabel);

            levelLabel = new Label()
            {
                Location = new Point(400, 10),
                Size = new Size(275, 30),
                Text = $"Niveau {levelCount}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(levelLabel);

            scoreLabel = new Label()
            {
                Location = new Point(720, 10),
                Size = new Size(400, 30),
                Text = $"Score : {scoreCount}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(scoreLabel);

            powerLabel = new Label()
            {
                Location = new Point(1150, 10),
                Size = new Size(80, 30),
                Text = "00:15",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(powerLabel);

            hearthPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(130, 80)
            };

            Controls.Add(hearthPanel);

            PictureBox hearth1 = Hearth(new Point(10, 10));
            hearthPanel.Controls.Add(hearth1);

            PictureBox hearth2 = Hearth(new Point(50, 10));
            hearthPanel.Controls.Add(hearth2);

            PictureBox hearth3 = Hearth(new Point(90, 10));
            hearthPanel.Controls.Add(hearth3);
        }

        private void PowerUsed()
        {
            switch (game.player.selectedPower)
            {
                case 1:
                    if (shieldPictureBox != null)
                    {
                        shieldPictureBox.Image?.Dispose();
                        shieldPictureBox.Dispose();
                        usedShieldPictureBox = ShieldUsed(new Point(1230, 5));
                        Controls.Add(usedShieldPictureBox);
                        scoreCount += 1000;
                        UpdateScoreLabel();
                    }
                    break;
                case 2:
                    if (damagePictureBox != null)
                    {
                        damagePictureBox.Image?.Dispose();
                        damagePictureBox.Dispose();
                        usedDamagePictureBox = DamageUsed(new Point(1230, 5));
                        Controls.Add(usedDamagePictureBox);
                        scoreCount += 1000;
                        UpdateScoreLabel();
                    }
                    break;
                case 3:
                    if (invisiblePictureBox != null)
                    {
                        invisiblePictureBox.Image?.Dispose();
                        invisiblePictureBox.Dispose();
                        usedInvisiblePictureBox = InvisibleUsed(new Point(1230, 5));
                        Controls.Add(usedInvisiblePictureBox);
                        scoreCount += 1000;
                        UpdateScoreLabel();
                    }
                    break;
            }
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
                scoreCount += 1000;
                UpdateScoreLabel();
                game.StartGame(e.level + 1);
                game.player.SetActualGame(game.ActualGame);
                LoadMap();
                LoadEntities();
                pnlGame.Visible = true;
            }
            else
            {
                // TODO: retour hub
            }
        }

        private void Player_DamageTaken(object? sender, DamageEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => Player_DamageTaken(sender, e)));
                return;
            }

            float playerHP = e.playerHP;

            int heartsToRemove = 0;

            if (playerHP >= 0)
            {
                heartsToRemove = 1;
                scoreCount -= 300;
            }

            for (int i = 0; i < heartsToRemove; i++)
            {
                int lastIndex = hearthPanel.Controls.Count - 1;
                if (lastIndex >= 0)
                {
                    hearthPanel.Controls.RemoveAt(lastIndex);
                }
            }

            UpdateScoreLabel();
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
            pnlGame.Location = new Point((ClientSize.Width - pnlGame.Width) / 2, (ClientSize.Height - pnlGame.Height) / 2); // Center window
            pnlGame.ResumeLayout();
        }

        public void Unload()
        {
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
            game.player.StopMovement();
            game.ActualGame = null;

            // Forcer le garbage collector pour libérer la mémoire
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void UpdateScoreLabel()
        { 
            scoreLabel.Invoke((MethodInvoker)delegate {
                scoreLabel.Text = $"Score : {scoreCount}";
            });

            // TODO

            // Pouvoirs
            // Kill des ennemis
        }

        private void Map_DoorOpen(object? sender, DoorOpenEventArgs e)
        {
            grid[e.DoorPos.x, e.DoorPos.y].Image = null;
        }

        private void Map_CoinEarn(object? sender, EarnCoinEventArgs e)
        {
            Map map = game.ActualGame.getMap();

            grid[e.Pos.x, e.Pos.y].Image = null;

            if (map.GetCoin(e.Pos.x, e.Pos.y))
            {
                coinCount += 1;
                scoreCount += 10;
                coinCounterLabel.Invoke((MethodInvoker)delegate {
                    coinCounterLabel.Text = coinCount.ToString();
                });
            }

            else if (map.GetBolt(e.Pos.x, e.Pos.y))
            {
                boltCount += 1;
                scoreCount += 100;
                boltCounterLabel.Invoke((MethodInvoker)delegate {
                    boltCounterLabel.Text = boltCount.ToString();
                });
            }
            UpdateScoreLabel();
        }

        public void LoadEntities()
        {
            enemy = game.ActualGame.GetEnemies();
            Vector2 playerpos = new Vector2(game.ActualGame.player.pos.y, game.ActualGame.player.pos.x);
            game.ActualGame.player.PositionChanged += Player_PositionChanged;
            game.ActualGame.player.DamageTaken += Player_DamageTaken;

            PBplayer = new PictureBox()
            {
                Location = new Point(playerpos.y * tileSize, playerpos.x * tileSize),
                Size = new Size(tileSize, tileSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\Pac-bot1.png")
            };
            pnlGame.Controls.Add(PBplayer);

            PBenemy = new PictureBox[enemy.Length];
            foreach (var en in enemy)
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
                            PowerUsed();
                            break;
                        }
                }
            }
        }
    }
}