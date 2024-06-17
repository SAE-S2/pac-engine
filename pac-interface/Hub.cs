using pac_engine;
namespace pac_interface
{
    public partial class Hub : Form
    {
        private Panel panel;
        private PictureBox pictureBoxGarde;
        private PictureBox pictureBoxIngenieur;
        private Label labelGarde;
        private Label labelIngenieur;
        private ToolTip toolTip;
        private PacBot actualGame;
        private int[] upgradesPrice;
        private int[] passivePrice;
        private int[] activePrice;

        public Hub(PacBot? game)
        {
            if (game != null)
                this.actualGame = game;
            else
            {
                this.actualGame = new PacBot("test", 1280, 720);
                this.actualGame.LoadWithProfil(1);
            }

        InitializeComponent();
            SetupControls(); // Call the method to add custom controls
        }

        private void SetupControls()
        {
            toolTip = new ToolTip();

            this.Text = "HUB";
            this.Size = new Size(1000, 600);
            this.BackgroundImage = Image.FromFile("..\\..\\..\\Resources\\Backgrounds\\BGPrison.jpg"); // Replace with the correct path
            this.BackgroundImageLayout = ImageLayout.Stretch;


            Label boltsNB = new Label
            {
                Text = "" + actualGame.player.bolts,
                Name = "boltsNB",
                Font = new Font("Arial", 16, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(1250, 50),
                Size = new Size(50, 20),
                BackColor = Color.Transparent
            };
            this.Controls.Add(boltsNB);

            PictureBox bolts = new PictureBox
            {
                Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png"),
                Size = new Size(50, 50),
                Location = new Point(1300, 38),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            this.Controls.Add(bolts);

            // Garde Label
            labelGarde = new Label
            {
                Text = "Garde",
                ForeColor = Color.White,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(100, 30),
                Location = new Point(240, 450),
                BackColor = Color.Transparent
            };
            this.Controls.Add(labelGarde);

            // Garde PictureBox
            pictureBoxGarde = new PictureBox
            {
                Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\Guard1.png"), // Replace with the correct path
                Size = new Size(250, 250),
                Location = new Point(150, 500),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            pictureBoxGarde.Click += new EventHandler(Launch_Click);
            this.Controls.Add(pictureBoxGarde);

            // Ingénieur Label
            labelIngenieur = new Label
            {
                Text = "Ingénieur",
                ForeColor = Color.White,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(125, 30),
                Location = new Point(875, 250),
                BackColor = Color.Transparent
            };
            this.Controls.Add(labelIngenieur);

            // Ingénieur PictureBox
            pictureBoxIngenieur = new PictureBox
            {
                Image = Image.FromFile("..\\..\\..\\Resources\\Entity\\Engineer1.png"), // Replace with the correct path
                Size = new Size(250, 250),
                Location = new Point(800, 300),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            pictureBoxIngenieur.Click += new EventHandler(PictureBoxIngenieur_Click);
            this.Controls.Add(pictureBoxIngenieur);

            panel = new Panel
            {
                Size = new Size(850, 500),
                Name = "panel",
                BackColor = Color.FromArgb(128, 0, 0, 0), // Semi-transparent background
                Visible = false
            };
            this.Controls.Add(panel);

            // Close button
            Button closeButton = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(panel.Width - 30, 10),
                Size = new Size(20, 20)
            };
            closeButton.Click += new EventHandler(CloseButton_Click);
            panel.Controls.Add(closeButton);

            // Title label
            Label labelTitle = new Label
            {
                Text = "INGENIEUR",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point((panel.Width - 200) / 2, 20),
                Size = new Size(200, 40),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(labelTitle);

            // Upgrades section
            GroupBox groupBoxUpgrades = new GroupBox
            {
                Text = "AMELIORATIONS",
                Name = "UP",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 80),
                Size = new Size(400, 400)
            };
            panel.Controls.Add(groupBoxUpgrades);

            string[] upgrades = { "+0,5 coeur", "+10% de vitesse", "+0,5 régenération" };
            int levelToGoHP = (int)((actualGame.player.maxHealth - 3) * 10) / 5 + 1;
            int levelToGoSpeed = (int)((actualGame.player.speed - 1) * 100) / 10 + 1;
            int levelToGoRegen = (int)((actualGame.player.regen - 0) * 10) / 5 + 1;
            int[] upgradesPriceS = { levelToGoHP, levelToGoSpeed, levelToGoRegen };
            upgradesPrice = upgradesPriceS;
            string[] upgradesDesc = { "Ajoute 0,5 coeur a votre vie total", "Ajoute 10% a votre vitesse de base", "Ajoute 0,5 coeur a la fin de chaque carte" };
            string[] upgradeIcons = { "health.png", "speed.png", "regen.png" }; // Replace with correct paths

            for (int i = 0; i < upgrades.Length; i++)
            {
                PictureBox icon = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\" + upgradeIcons[i]),
                    Size = new Size(40, 40),
                    Location = new Point(15, 75 + i * 100),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(icon);
                toolTip.SetToolTip(icon, upgradesDesc[i]);

                Label labelUpgrade = new Label
                {
                    Text = $"{upgrades[i]} (lvl. {(i == 0 ? levelToGoHP - 1 : (i == 1 ? levelToGoSpeed - 1 : levelToGoRegen - 1))})",
                    Name = "label"+i,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 80 + i * 100),
                    Size = new Size(200, 20),
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(labelUpgrade);
                toolTip.SetToolTip(labelUpgrade, upgradesDesc[i]);

                Label labelPrice = new Label
                {
                    Text = "" + upgradesPrice[i],
                    Name = "price" + i,
                    Font = new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 100 + i * 100),
                    Size = new Size(10, 20),
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(labelPrice);
                toolTip.SetToolTip(labelPrice, upgradesDesc[i]);

                PictureBox boulons = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png"),
                    Size = new Size(20, 20),
                    Location = new Point(70, 100 + i * 100),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(boulons);
                toolTip.SetToolTip(boulons, upgradesDesc[i]);

                Button buttonBuy = new Button
                {
                    Text = "Acheter",
                    Tag = upgrades[i],
                    Location = new Point(260, 80 + i * 100),
                    Size = new Size(100, 30),
                    BackColor = Color.Blue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                buttonBuy.Click += new EventHandler(ButtonBuy_Click);
                groupBoxUpgrades.Controls.Add(buttonBuy);
                toolTip.SetToolTip(buttonBuy, upgradesDesc[i]);
            }

            // Passive Powers section
            GroupBox groupBoxPassive = new GroupBox
            {
                Text = "POUVOIR PASSIF",
                Name = "PS",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(450, 80),
                Size = new Size(380, 180)
            };
            panel.Controls.Add(groupBoxPassive);

            string[] passivePowers = { "PEUREUX", "CHANCEUX" };
            int levelToGoLucky = actualGame.player.lucky / 10 + 1;
            int levelToGoPeur = actualGame.player.peureux / 10 + 1;
            int[] passivePriceS = { levelToGoPeur, levelToGoLucky };
            passivePrice = passivePriceS;
            string[] passivePowersDesc = { "Quand un ennemi est proche +10% de vitesse", "Vous avez 10% de chance d’avoir une pièce bonus quand vous en récupérez." };
            string[] passiveIcons = { "peur.png", "luck.png" }; // Replace with correct paths

            for (int i = 0; i < passivePowers.Length; i++)
            {
                PictureBox icon = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\" + passiveIcons[i]),
                    Size = new Size(40, 40),
                    Location = new Point(15, 35 + i * 75),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxPassive.Controls.Add(icon);
                toolTip.SetToolTip(icon, passivePowersDesc[i]);

                Label labelPassive = new Label
                {
                    Text = $"{passivePowers[i]} (lvl. {(i == 0 ? levelToGoLucky - 1 : levelToGoPeur - 1)})",
                    Name = "label"+i,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 40 + i * 75),
                    Size = new Size(125, 20),
                    BackColor = Color.Transparent
                };
                groupBoxPassive.Controls.Add(labelPassive);
                toolTip.SetToolTip(labelPassive, passivePowersDesc[i]);

                Label labelPrice = new Label
                {
                    Text = ""+ passivePrice[i],
                    Name = "price" + i,
                    Font = new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 60 + i * 75),
                    Size = new Size(10, 20),
                    BackColor = Color.Transparent
                };
                groupBoxPassive.Controls.Add(labelPrice);
                toolTip.SetToolTip(labelPrice, passivePowersDesc[i]);

                PictureBox boulons = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png"),
                    Size = new Size(20, 20),
                    Location = new Point(70, 60 + i * 75),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxPassive.Controls.Add(boulons);
                toolTip.SetToolTip(boulons, passivePowersDesc[i]);

                Button buttonBuy = new Button
                {
                    Text = "Acheter",
                    Tag = passivePowers[i],
                    Location = new Point(240, 35 + i * 75),
                    Size = new Size(100, 30),
                    BackColor = Color.Blue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                buttonBuy.Click += new EventHandler(ButtonBuy_Click);
                groupBoxPassive.Controls.Add(buttonBuy);
                toolTip.SetToolTip(buttonBuy, passivePowersDesc[i]);
            }

            // Active Powers section
            GroupBox groupBoxActive = new GroupBox
            {
                Text = "POUVOIR ACTIF",
                Name = "PA",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(450, 280),
                Size = new Size(380, 200)
            };
            panel.Controls.Add(groupBoxActive);

            string[] activePowers = { "Bouclier", "Dégats", "Invisibilité" };
            int[] activePriceS = { actualGame.player.shield.level + 1, actualGame.player.damagePower.level + 1, actualGame.player.invisible.level + 1 };
            activePrice = activePriceS;
            string[] activePowersDesc = { "+1 cœur pendant 30 secondes", "Tuer les ennemis au toucher pendant 10 secondes", "Invisibilité pendant 10 secondes" };
            string[] activeIcons = { "Bouclier.png", "Degats.png", "Invisible.png" }; // Replace with correct paths

            for (int i = 0; i < activePowers.Length; i++)
            {
                PictureBox icon = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\" + activeIcons[i]),
                    Size = new Size(40, 40),
                    Location = new Point(15, 30 + i * 60),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxActive.Controls.Add(icon);
                toolTip.SetToolTip(icon, activePowersDesc[i]);

                Label labelActive = new Label
                {
                    Text = $"{activePowers[i]} (lvl. {(i == 0 ? actualGame.player.shield.level : (i == 1 ? actualGame.player.damagePower.level : actualGame.player.invisible.level))})",
                    Name = "label"+i,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 35 + i * 60),
                    Size = new Size(110, 20),
                    BackColor = Color.Transparent
                };
                groupBoxActive.Controls.Add(labelActive);
                toolTip.SetToolTip(labelActive, activePowersDesc[i]);


                Label labelPrice = new Label
                {
                    Text = ""+ activePrice[i],
                    Name = "price" + i,
                    Font = new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 55 + i * 60),
                    Size = new Size(10, 20),
                    BackColor = Color.Transparent
                };
                groupBoxActive.Controls.Add(labelPrice);
                toolTip.SetToolTip(labelPrice, activePowersDesc[i]);

                PictureBox boulons = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png"),
                    Size = new Size(20, 20),
                    Location = new Point(70, 55 + i * 60),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxActive.Controls.Add(boulons);
                toolTip.SetToolTip(boulons, activePowersDesc[i]);

                Button buttonBuy = new Button
                {
                    Text = "Acheter",
                    Tag = activePowers[i],
                    Location = new Point(170, 35 + i * 60),
                    Size = new Size(100, 30),
                    BackColor = Color.Blue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                buttonBuy.Click += new EventHandler(ButtonBuy_Click);
                groupBoxActive.Controls.Add(buttonBuy);
                toolTip.SetToolTip(buttonBuy, activePowersDesc[i]);


                Button buttonEquip = new Button
                {
                    Text = "Equiper",
                    Tag = activePowers[i],
                    Location = new Point(275, 35 + i * 60),
                    Size = new Size(100, 30),
                    BackColor = Color.Blue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                buttonEquip.Click += new EventHandler(ButtonEquip_Click);

                groupBoxActive.Controls.Add(buttonEquip);
                toolTip.SetToolTip(buttonEquip, activePowersDesc[i]);
            }

            // Center panel on the form
            panel.Location = new Point((this.ClientSize.Width - panel.Width) / 2, (this.ClientSize.Height - panel.Height) / 2);
            panel.Anchor = AnchorStyles.None;
        }

        private void PictureBoxGarde_Click(object sender, EventArgs e)
        {
            // Show or hide the panel and the other elements
            panel.Visible = !panel.Visible;
            pictureBoxGarde.Visible = !panel.Visible;
            pictureBoxIngenieur.Visible = !panel.Visible;
            labelGarde.Visible = !panel.Visible;
            labelIngenieur.Visible = !panel.Visible;
        }

        private void PictureBoxIngenieur_Click(object sender, EventArgs e)
        {
            // Show or hide the panel and the other elements
            panel.Visible = !panel.Visible;
            pictureBoxGarde.Visible = !panel.Visible;
            pictureBoxIngenieur.Visible = !panel.Visible;
            labelGarde.Visible = !panel.Visible;
            labelIngenieur.Visible = !panel.Visible;
        }

        Game game;
        private void Launch_Click(object sender, EventArgs e)
        {
            actualGame.initializeGame();
            if (game == null)
            {
                game = new Game(actualGame);
                this.Visible = false;
                game.Show();
                game.WindowState = FormWindowState.Maximized;
                game.FormClosed += Game_FormClosed;
            }
            else
            {
                game.Activate();
            }
            actualGame.player.Health = actualGame.player.maxHealth;
            game.LoadMap();
            game.LoadEntities();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            // Hide the panel and show the other elements
            panel.Visible = false;
            pictureBoxGarde.Visible = true;
            pictureBoxIngenieur.Visible = true;
            labelGarde.Visible = true;
            labelIngenieur.Visible = true;
        }

        private void ButtonEquip_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string item = button.Tag.ToString();
            switch (item)
            {
                case "Bouclier":
                    if (actualGame.player.shield.level < 1)
                    {
                        MessageBox.Show("Vous n'avez pas le pouvoir");
                        break;
                    }
                    actualGame.player.selectedPower = 1;
                    MessageBox.Show("Vous avez bien équipé: " + item);
                    break;
                case "Dégats":
                    if (actualGame.player.damagePower.level < 1)
                    {
                        MessageBox.Show("Vous n'avez pas le pouvoir");
                        break;
                    }
                    actualGame.player.selectedPower = 2;
                    MessageBox.Show("Vous avez bien équipé: " + item);
                    break;
                case "Invisibilité":
                    if (actualGame.player.invisible.level < 1)
                    {
                        MessageBox.Show("Vous n'avez pas le pouvoir");
                        break;
                    }
                    actualGame.player.selectedPower = 3;
                    MessageBox.Show("Vous avez bien équipé: " + item);
                    break;
            }
        }

        private void ButtonBuy_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string item = button.Tag.ToString();
            switch (item)
            {
                case "+0,5 coeur":
                    if (upgradesPrice[0] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (upgradesPrice[0] > 4)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= upgradesPrice[0];
                    upgradesPrice[0]++;
                    this.Controls["panel"].Controls["UP"].Controls["price0"].Text = "" + upgradesPrice[0];
                    this.Controls["panel"].Controls["UP"].Controls["label0"].Text = Text = $"+0,5 coeur (lvl. {upgradesPrice[0] - 1})";
                    actualGame.player.maxHealth += 0.5f;
                    actualGame.player.Health += 0.5f;
                    break;
                case "+10% de vitesse":
                    if (upgradesPrice[1] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (upgradesPrice[1] > 4)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= upgradesPrice[1];
                    upgradesPrice[1]++;
                    this.Controls["panel"].Controls["UP"].Controls["price1"].Text = "" + upgradesPrice[1];
                    this.Controls["panel"].Controls["UP"].Controls["label1"].Text = Text = $"+10% de vitesse (lvl. {upgradesPrice[1] - 1})";
                    actualGame.player.speed += 0.1f;
                    break;
                case "+0,5 régenération":
                    if (upgradesPrice[2] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (upgradesPrice[2] > 4)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= upgradesPrice[2];
                    upgradesPrice[2]++;
                    this.Controls["panel"].Controls["UP"].Controls["price2"].Text = "" + upgradesPrice[2];
                    this.Controls["panel"].Controls["UP"].Controls["label2"].Text = Text = $"+0,5 régenération (lvl. {upgradesPrice[2] - 1})";
                    actualGame.player.regen += 0.5f;
                    break;
                case "PEUREUX":
                    if (passivePrice[0] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (passivePrice[0] > 3)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= passivePrice[0];
                    passivePrice[0]++;
                    this.Controls["panel"].Controls["PS"].Controls["price0"].Text = "" + passivePrice[0];
                    this.Controls["panel"].Controls["PS"].Controls["label0"].Text = Text = $"PEUREUX (lvl. {passivePrice[0] - 1})";
                    actualGame.player.peureux += 10;
                    break;
                case "CHANCEUX":
                    if (passivePrice[1] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (passivePrice[1] > 3)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= passivePrice[1];
                    passivePrice[1]++;
                    this.Controls["panel"].Controls["PS"].Controls["price1"].Text = "" + passivePrice[1];
                    this.Controls["panel"].Controls["PS"].Controls["label1"].Text = Text = $"CHANCEUX (lvl. {passivePrice[1] - 1})";
                    actualGame.player.lucky += 10;
                    break;
                case "Bouclier":
                    if (activePrice[0] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (activePrice[0] > 3)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= activePrice[0];
                    activePrice[0]++;
                    this.Controls["panel"].Controls["PA"].Controls["price0"].Text = "" + activePrice[0];
                    this.Controls["panel"].Controls["PA"].Controls["label0"].Text = Text = $"Bouclier (lvl. {activePrice[0] - 1})";
                    actualGame.player.shield.level += 1;
                    break;
                case "Dégats":
                    if (activePrice[1] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (activePrice[1] > 3)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= activePrice[1];
                    activePrice[1]++;
                    this.Controls["panel"].Controls["PA"].Controls["price1"].Text = "" + activePrice[1];
                    this.Controls["panel"].Controls["PA"].Controls["label1"].Text = Text = $"Dégats (lvl. {activePrice[1] - 1})";
                    actualGame.player.damagePower.level += 1;
                    break;
                case "Invisibilité":
                    if (activePrice[2] > actualGame.player.bolts)
                    {
                        MessageBox.Show("Vous n'avez pas assez de boulons");
                        break;
                    }
                    else if (activePrice[2] > 3)
                    {
                        MessageBox.Show("Vous ne pouvez plus ameliorer");
                        break;
                    }
                    actualGame.player.bolts -= activePrice[2];
                    activePrice[2]++;
                    this.Controls["panel"].Controls["PA"].Controls["price2"].Text = "" + activePrice[2];
                    this.Controls["panel"].Controls["PA"].Controls["label2"].Text = Text = $"Invisibilité (lvl. {activePrice[2] - 1})";
                    actualGame.player.invisible.level += 1;
                    break;
            }
            this.Controls["boltsNB"].Text = ""+actualGame.player.bolts;
        }

        private void Game_FormClosed(object? sender, FormClosedEventArgs e)
        {
            game = null;
            this.Show();
        }
    }
}
