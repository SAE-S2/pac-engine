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
        public Game()
        {
            InitializeComponent();
        }

        private PictureBox placeWall(Point coords,int level, int type)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(tileSize, tileSize),
                Image = Image.FromFile("..\\..\\..\\Resources\\murs\\Level" + level.ToString() + "\\type" + type.ToString() + ".png")
            };
            return pictureBox;
        }

        public void LoadMap(PacBot game)
        {
            Map map = game.ActualGame.getMap();
            Vector2 startpos = new Vector2(0);
            int maxY = map.map.GetLength(0);
            int maxX = map.map.GetLength(1);
            PictureBox[,] grid = new PictureBox[maxY, maxX];

            Size = new Size((maxX + 1) * tileSize, (maxY + 1) * tileSize);
            for (int line = 0; line < maxY; line++)
            {
                for (int col = 0; col < maxX; col++)
                {
                    if (map.GetWall(line, col))
                    {
                        grid[line, col] = placeWall(new Point(col * tileSize, line * tileSize), game.ActualGame.level,map.GetWallType(line,col)); // LEVEL
                    }
                    else if (map.GetCoin(line, col))
                    {
                        grid[line, col] = new PictureBox()
                        {
                            Location = new Point(col * tileSize, line * tileSize),
                            Size = new Size(tileSize, tileSize),
                            Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Coin.png")
                        };
                    
                    }
                    else if (map.GetBolt(line, col))
                    {
                        grid[line, col] = new PictureBox()
                        {
                            Location = new Point(col * tileSize, line * tileSize),
                            Size = new Size(tileSize, tileSize),
                            Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png")
                        };

                    }
                    Controls.Add(grid[line, col]);
                }
            }
            //Controls.Remove(grid[line, col]); -> Retirer une picturebox
        }
    }
}
