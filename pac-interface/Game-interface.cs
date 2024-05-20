﻿using pac_engine;
using pac_engine.Core;
using pac_engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        public const int tileSize = 128;
        private int coinCount = 0;
        private Label coinCounterLabel;
        private int boltCount = 0;
        private Label boltCounterLabel;
        private PacBot game;
        private PictureBox[,] grid;
        private PictureBox PBplayer;
        private PictureBox[] PBenemy;
        private Panel mapPanel;
        private Label levelLabel;
        private int levelCount;
        private Label scoreLabel;
        private int scoreCount;
        private Label powerLabel;

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

        private PictureBox Power(Point coords)
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

        private void InitializeElements()
        {
            levelCount = 1;
            scoreCount = 1000;

            PictureBox power = Power(new Point(1230, 5));
            Controls.Add(power);

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
                Text = $"Score {scoreCount}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(scoreLabel);

            powerLabel = new Label()
            {
                Location = new Point(1150, 10),
                Size = new Size(400, 30),
                Text = "00:15",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            Controls.Add(powerLabel);

            PictureBox hearth1 = Hearth(new Point(10, 10));
            Controls.Add(hearth1);

            PictureBox hearth2 = Hearth(new Point(50, 10));
            Controls.Add(hearth2);

            PictureBox hearth3 = Hearth(new Point(90, 10));
            Controls.Add(hearth3);
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

        private void Player_DamageTaken(object? sender, DamageEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LoadMap()
        {
            Map map = game.ActualGame.getMap();
            map.CoinEarn += Map_CoinEarn;
            Vector2 startpos = new Vector2(0); //TOP-LEFT corner
            int maxY = map.map.GetLength(0);
            int maxX = map.map.GetLength(1);
            grid = new PictureBox[maxY, maxX];
            

            mapPanel = new Panel
            {
                Location = new Point(200, 60),
                Size = new Size(maxX * tileSize, maxY * tileSize),
                BorderStyle = BorderStyle.FixedSingle 
            };

            Controls.Add(mapPanel);

            for (int line = 0; line < maxY; line++)
            {
                for (int col = 0; col < maxX; col++)
                {
                    if (map.GetWall(line, col))
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
                    mapPanel.Controls.Add(grid[line, col]);
                }
            }
            //Controls.Remove(grid[line, col]); -> Retirer une picturebox
        }

        private void Map_CoinEarn(object? sender, EarnCoinEventArgs e)
        {
            Map map = game.ActualGame.getMap();

            grid[e.Pos.x, e.Pos.y].Image = null;

            if (map.GetCoin(e.Pos.x, e.Pos.y))
            {
                coinCount += 1;
                coinCounterLabel.Invoke((MethodInvoker)delegate {
                    coinCounterLabel.Text = coinCount.ToString();
                });
            }

            else if (map.GetBolt(e.Pos.x, e.Pos.y))
            {
                boltCount += 1;
                boltCounterLabel.Invoke((MethodInvoker)delegate {
                    boltCounterLabel.Text = boltCount.ToString();
                });
            }
        }

        public void LoadEntities()
        {
            Entity[] enemy = game.ActualGame.GetEnemies();
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
            mapPanel.Controls.Add(PBplayer);

            PBenemy = new PictureBox[enemy.Length];
            int i = 0;
             while (enemy[i] != null) 
            {
                enemy[i].PositionChanged += Enemy_PositionChanged;
                PBenemy[i] = new PictureBox()
                {
                    Location = new Point(enemy[i].pos.y * tileSize, enemy[i].pos.x * tileSize),
                    Size = new Size(tileSize, tileSize),
                    SizeMode= PictureBoxSizeMode.Zoom,
                    Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\BasicEnnemi1-1.png")
                };
                grid[enemy[i].pos.x, enemy[i].pos.y].Visible = false;
                mapPanel.Controls.Add(PBenemy[i]);
                i++;
            }
        }

        private void Player_PositionChanged(object? sender, PositionChangedEventArgs player)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Player_PositionChanged(sender, player); }));
                return;
            }
            if (grid[player.OldPos.x, player.OldPos.y] != null)
            {
                grid[player.OldPos.x, player.OldPos.y].Visible = true;
            }
            if (grid[player.NewPos.x, player.NewPos.y] != null)
            {
                grid[player.NewPos.x, player.NewPos.y].Visible = false;
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

            grid[enemy.OldPos.x, enemy.OldPos.y].Visible = true;
            grid[enemy.NewPos.x, enemy.NewPos.y].Visible = false;
            PBenemy[0].Location = new Point(enemy.NewPos.y * tileSize, enemy.NewPos.x * tileSize);
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
            }
        }
    }
}