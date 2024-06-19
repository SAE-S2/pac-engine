﻿using pac_engine;
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
        public int tileSize = 48;
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
        private Panel HeartPanel;
        private PictureBox? shieldPictureBox;
        private PictureBox? usedShieldPictureBox;
        private PictureBox? invisiblePictureBox;
        private PictureBox? usedInvisiblePictureBox;
        private PictureBox? damagePictureBox;
        private PictureBox? usedDamagePictureBox;
        private System.Windows.Forms.Timer powerTimer;
        private Hub hub;
        public Game(Hub hub,PacBot game)
        {
            this.hub = hub;
            InitializeComponent();
            this.game = game;

            powerTimer = new System.Windows.Forms.Timer();
            powerTimer.Interval = 1000;
            powerTimer.Tick += PowerTimer_Tick;
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

        private PictureBox Heart(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(30, 30),
                Image = Image.FromFile("..\\..\\..\\Resources\\Coeur\\Coeur2.png"),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private PictureBox HalfHeart(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(30, 30),
                Image = Image.FromFile("..\\..\\..\\Resources\\Coeur\\Demi-Coeur2.png"),
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
                case 0:
                    break;
                case 1:
                    shieldPictureBox = Shield(new Point(1205, 5));
                    Controls.Add(shieldPictureBox);
                    break;
                case 2:
                    damagePictureBox = Damage(new Point(1205, 5));
                    Controls.Add(damagePictureBox);
                    break;
                case 3:
                    invisiblePictureBox = Invisible(new Point(1205, 5));
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
                Size = new Size(300, 30),
                Text = $"Score : {scoreCount}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(scoreLabel);
            if (game.player.selectedPower > 0)
            {
                powerLabel = new Label()
                {
                    Location = new Point(1110, 10),
                    Size = new Size(95, 30),
                    Text = "Press A",
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.Black
                };
                Controls.Add(powerLabel);
            }
            HeartPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(250, 80)
            };
            Controls.Add(HeartPanel);

            InitializeHearts(game.player.maxHealth);
        }

        PictureBox Heart0p5;
        PictureBox Heart1;
        PictureBox Heart1p5;
        PictureBox Heart2;
        PictureBox Heart2p5;
        PictureBox Heart3;
        PictureBox Heart3p5;
        PictureBox Heart4;
        PictureBox Heart4p5;
        PictureBox Heart5;
        PictureBox Heart5p5;
        PictureBox Heart6;
        PictureBox Heart6p5;
        private void InitializeHearts(float health)
        {

            Heart1 = Heart(new Point(10, 10));
            HeartPanel.Controls.Add(Heart1);
            if (health < 0.9f)
                Heart1.Visible = false;
            Heart0p5 = HalfHeart(new Point(10, 10));
            HeartPanel.Controls.Add(Heart0p5);
            if (health < 0.4f)
                Heart0p5.Visible = false;

            Heart2 = Heart(new Point(50, 10));
            HeartPanel.Controls.Add(Heart2);
            if (health < 1.9f)
                Heart2.Visible = false;
            Heart1p5 = HalfHeart(new Point(50, 10));
            HeartPanel.Controls.Add(Heart1p5);
            if (health < 1.4f)
                Heart1p5.Visible = false;

            Heart3 = Heart(new Point(90, 10));
            HeartPanel.Controls.Add(Heart3);
            if (health < 2.9f)
                Heart3.Visible = false;
            Heart2p5 = HalfHeart(new Point(90, 10));
            HeartPanel.Controls.Add(Heart2p5);
            if (health < 2.4f)
                Heart2p5.Visible = false;

            Heart4 = Heart(new Point(130, 10));
            HeartPanel.Controls.Add(Heart4);
            if (health < 3.9f)
                Heart4.Visible = false;
            Heart3p5 = HalfHeart(new Point(130, 10));
            HeartPanel.Controls.Add(Heart3p5);
            if (health < 3.4f)
                Heart3p5.Visible = false;

            Heart5 = Heart(new Point(170, 10));
            HeartPanel.Controls.Add(Heart5);
            if (health < 4.9f)
                Heart5.Visible = false;
            Heart4p5 = HalfHeart(new Point(170, 10));
            HeartPanel.Controls.Add(Heart4p5);
            if (health < 4.4f)
                Heart4p5.Visible = false;

            Heart6 = Heart(new Point(210, 10));
            HeartPanel.Controls.Add(Heart6);
            if (health < 5.9f)
                Heart6.Visible = false;
            Heart5p5 = HalfHeart(new Point(210, 10));
            HeartPanel.Controls.Add(Heart5p5);
            if (health < 5.4f)
                Heart5p5.Visible = false;

            Heart6p5 = Heart(new Point(250, 10));
            HeartPanel.Controls.Add(Heart6p5);
            if (health < 6.4f)
                Heart6p5.Visible = false;
        }

        private void UpdateHearts()
        {
            float health = game.player.Health + game.player.absorption;

            Heart0p5.Visible = true;
            Heart1.Visible = true;
            Heart1p5.Visible = true;
            Heart2.Visible = true;
            Heart2p5.Visible = true;
            Heart3.Visible = true;
            Heart3p5.Visible = true;
            Heart4.Visible = true;
            Heart4p5.Visible = true;
            Heart5.Visible = true;
            Heart5p5.Visible = true;
            Heart6.Visible = true;
            Heart6p5.Visible = true;

            if (health < 0.4f)
                Heart0p5.Visible = false;
            if (health < 0.9f)
                Heart1.Visible = false;
            if (health < 1.4f)
                Heart1p5.Visible = false;
            if (health < 1.9f)
                Heart2.Visible = false;
            if (health < 2.4f)
                Heart2p5.Visible = false;
            if (health < 2.9f)
                Heart3.Visible = false;
            if (health < 3.4f)
                Heart3p5.Visible = false;
            if (health < 3.9f)
                Heart4.Visible = false;
            if (health < 4.4f)
                Heart4p5.Visible = false;
            if (health < 4.9f)
                Heart5.Visible = false;
            if (health < 5.4f)
                Heart5p5.Visible = false;
            if (health < 5.9f)
                Heart6.Visible = false;
            if (health < 6.4f)
                Heart6p5.Visible = false;
        }

        private void StartPowerTimer(int duration)
        {
            powerTimer.Stop();
            powerTimer.Tag = duration;
            powerTimer.Start();
        }

        private void PowerTimer_Tick(object sender, EventArgs e)
        {
            int remainingSeconds = (int)powerTimer.Tag;
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                powerTimer.Stop();
                powerLabel.Text = "Press A";

                HeartPanel.Controls.Clear();
                InitializeHearts(game.player.Health);

                switch (game.player.selectedPower)
                {
                    case 1:
                        if (shieldPictureBox != null)
                        {
                            usedShieldPictureBox.Image?.Dispose();
                            usedShieldPictureBox.Dispose();
                            shieldPictureBox = shieldPictureBox = Shield(new Point(1205, 5));
                            Controls.Add(shieldPictureBox);
                        }
                        break;
                    case 2:
                        if (damagePictureBox != null)
                        {
                            usedDamagePictureBox.Image?.Dispose();
                            usedDamagePictureBox.Dispose();
                            damagePictureBox = damagePictureBox = Damage(new Point(1205, 5));
                            Controls.Add(damagePictureBox);
                        }
                        break;
                    case 3:
                        if (invisiblePictureBox != null)
                        {
                            usedInvisiblePictureBox.Image?.Dispose();
                            usedInvisiblePictureBox.Dispose();
                            invisiblePictureBox = invisiblePictureBox = Invisible(new Point(1205, 5));
                            Controls.Add(invisiblePictureBox);
                        }
                        break;
                    default:
                        break;
                }

            }
            else
            {
                TimeSpan time = TimeSpan.FromSeconds(remainingSeconds);
                powerLabel.Text = time.ToString(@"mm\:ss");
                powerTimer.Tag = remainingSeconds;
            }
        }
        private void PowerUsed()
        {
            if (powerTimer.Enabled)
            {
                return;
            }

            int timerDuration;

            switch (game.player.selectedPower)
            {
                case 1:
                    if (shieldPictureBox != null)
                    {
                        timerDuration = game.player.damagePower.GetLevel() == 1 ? 180 : 240;
                        StartPowerTimer(timerDuration);
                        shieldPictureBox.Image?.Dispose();
                        shieldPictureBox.Dispose();
                        usedShieldPictureBox = ShieldUsed(new Point(1205, 5));
                        Controls.Add(usedShieldPictureBox);
                        scoreCount += 250;
                        UpdateScoreLabel();
                        UpdateHearts();
                    }
                    break;
                case 2:
                    if (damagePictureBox != null)
                    {
                        timerDuration = game.player.invisible.GetLevel() == 1 ? 180 : 240;
                        StartPowerTimer(timerDuration);
                        damagePictureBox.Image?.Dispose();
                        damagePictureBox.Dispose();
                        usedDamagePictureBox = DamageUsed(new Point(1205, 5));
                        Controls.Add(usedDamagePictureBox);
                        scoreCount += 250;
                        UpdateScoreLabel();
                    }
                    break;
                case 3:
                    if (invisiblePictureBox != null)
                    {
                        timerDuration = game.player.shield.GetLevel() == 1 ? 180 : 240;
                        StartPowerTimer(timerDuration);
                        invisiblePictureBox.Image?.Dispose();
                        invisiblePictureBox.Dispose();
                        usedInvisiblePictureBox = InvisibleUsed(new Point(1205, 5));
                        Controls.Add(usedInvisiblePictureBox);
                        scoreCount += 250;
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
                game.player.Heal(game.player.regen);
                game.player.SetActualGame(game.ActualGame);
                LoadMap();
                LoadEntities();
                pnlGame.Visible = true;
            }
            else
            {
                this.Visible = false;
                hub.Show();
            }
        }

        private void Player_DamageTaken(object? sender, DamageEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => Player_DamageTaken(sender, e)));
                return;
            }
            UpdateHearts();
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
            pnlGame.Location = new Point( (ClientSize.Width - pnlGame.Width) / 2, (ClientSize.Height - pnlGame.Height) / 2); // Center window
            pnlGame.ResumeLayout();
        }

        public void Unload()
        {
            game.ActualGame.player.StopMovement();
            // Désabonnement aux events
            game.ActualGame.player.PositionChanged -= Player_PositionChanged;
            game.ActualGame.player.DamageTaken -= Player_DamageTaken;

            if (enemy != null)
            {
                foreach (var en in enemy)
                {
                    if (en != null)
                    {
                        en.PositionChanged -= Enemy_PositionChanged;
                        en.Killed -= Enemy_Killed;
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

            if (grid[e.Pos.x, e.Pos.y] != null)
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
            Vector2 playerpos = new Vector2(game.ActualGame.map.spawn.y, game.ActualGame.map.spawn.x);
            game.ActualGame.player.PositionChanged += Player_PositionChanged;
            game.ActualGame.player.DamageTaken += Player_DamageTaken;

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
                    en.Killed += Enemy_Killed;
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
        private void Enemy_Killed(object? sender, KilledEventArgs enemy)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Enemy_Killed(sender, enemy); }));
                return;
            }
            PBenemy[enemy.id].Dispose();
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
                            if (game.ActualGame.player.selectedPower != 0)
                            {
                                game.ActualGame.player.ActivePower();
                                PowerUsed();
                            }
                            break;
                        }
                }
            }
        }
    }
}
