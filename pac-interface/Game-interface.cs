using pac_engine;
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
        public const int tileSize = 64;
        private PacBot game;
        private PictureBox[,] grid;
        private PictureBox PBplayer;
        private PictureBox[] PBenemy;
        public Game(PacBot game)
        {
            InitializeComponent();
            this.game = game;
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
            Map map = game.ActualGame.getMap();
            Vector2 startpos = new Vector2(0); //TOP-LEFT corner
            int maxY = map.map.GetLength(0);
            int maxX = map.map.GetLength(1);
             grid = new PictureBox[maxY, maxX];

            Size = new Size((maxX + 1) * tileSize, (maxY + 1) * tileSize);
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
                    Controls.Add(grid[line, col]);
                }
            }
            //Controls.Remove(grid[line, col]); -> Retirer une picturebox
        }
        public void LoadEntities()
        {
            Entity[] enemy = game.ActualGame.GetEnemies();
            Vector2 playerpos = new Vector2(game.ActualGame.player.pos.y, game.ActualGame.player.pos.x);
            game.ActualGame.player.PositionChanged += Player_PositionChanged;

            PBplayer = new PictureBox()
            {
                Location = new Point(playerpos.y * tileSize, playerpos.x * tileSize),
                Size = new Size(tileSize, tileSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\Pac-bot1.png")
            };
            Controls.Add(PBplayer);

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
                Controls.Add(PBenemy[i]);
                i++;
            }
        }

        private void Player_PositionChanged(object? sender, PositionChangedEventArgs player)
        {
            grid[player.OldPos.y, player.OldPos.x].Visible = true;
            grid[player.NewPos.y, player.NewPos.x].Visible = false;
            PBplayer.Location = new Point(player.NewPos.y * tileSize, player.NewPos.x * tileSize);
        }

        private void Enemy_PositionChanged(object? sender, PositionChangedEventArgs enemy)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Enemy_PositionChanged(sender, enemy); }));
                return;
            }

            grid[enemy.OldPos.y, enemy.OldPos.x].Visible = true;
            grid[enemy.NewPos.y, enemy.NewPos.x].Visible = false;
            PBenemy[0].Location = new Point(enemy.NewPos.y * tileSize, enemy.NewPos.x * tileSize);
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
                case Keys.Q:
                    {
                        game.ActualGame.player.AngleChange(1); //Gauche
                        break;
                    }
                case Keys.S:
                    {
                        game.ActualGame.player.AngleChange(2); //Bas
                        break;
                    }
                case Keys.D:
                    {
                        game.ActualGame.player.AngleChange(3); //Droite
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
