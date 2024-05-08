using pac_engine;
using pac_engine.Core;
using pac_engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void placeWall(Point coords)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Location = coords,
                Size = new Size(tileSize, tileSize),
                Image = Image.FromFile("C:\\Users\\torna\\OneDrive\\Documents\\GitHub\\pac-engine\\pac-interface\\Resources\\murs\\Level1\\Level1 Mur1_2Coin.png")
            };
            Controls.Add(pictureBox);
        }

        public void LoadMap(PacBot game)
        {
            Map map = game.ActualGame.getMap();
            Vector2 startpos = new Vector2(0);
            int maxY = map.map.GetLength(0);
            int maxX = map.map.GetLength(1);
            Size = new Size((maxX+1) * tileSize, (maxY+1) * tileSize);
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (map.GetWall(y,x)) 
                    {
                        placeWall(new Point(x * tileSize, y * tileSize));
                    }
                }    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            PacBot test = new PacBot("test", 1280, 720);
            test.initializeGame();
            LoadMap(test);
        }
    }
}
