using System;
using System.Drawing;
using System.Windows.Forms;

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

        public Hub()
        {
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
            pictureBoxGarde.Click += new EventHandler(PictureBoxGarde_Click);
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
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 80),
                Size = new Size(400, 400)
            };
            panel.Controls.Add(groupBoxUpgrades);

            string[] upgrades = { "+0,5 coeur", "+5% de vitesse", "+12,5% du champ de vision", "+0,5 coeur de régenération" };
            string[] upgradesDesc = { "Ajoute 0,5 coeur a votre vie total", "Ajoute 5% a votre vitesse de base", "Augmente votre champ de vision de 12,5%", "Ajoute 0,5 coeur a la fin de chaque carte" };
            string[] upgradeIcons = { "Bouclier.png", "Bouclier.png", "Bouclier.png", "Bouclier.png" }; // Replace with correct paths

            for (int i = 0; i < upgrades.Length; i++)
            {
                PictureBox icon = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Pouvoirs\\" + upgradeIcons[i]),
                    Size = new Size(40, 40),
                    Location = new Point(15, 55 + i * 90),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(icon);
                toolTip.SetToolTip(icon, upgradesDesc[i]);

                Label labelUpgrade = new Label
                {
                    Text = upgrades[i],
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 60 + i * 90),
                    Size = new Size(200, 20),
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(labelUpgrade);
                toolTip.SetToolTip(labelUpgrade, upgradesDesc[i]);

                Label labelPrice = new Label
                {
                    Text = "5",
                    Font = new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 80 + i * 90),
                    Size = new Size(10, 20),
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(labelPrice);
                toolTip.SetToolTip(labelPrice, upgradesDesc[i]);

                PictureBox boulons = new PictureBox
                {
                    Image = Image.FromFile("..\\..\\..\\Resources\\Monnaies\\Boulon.png"),
                    Size = new Size(20, 20),
                    Location = new Point(70, 80 + i * 90),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                groupBoxUpgrades.Controls.Add(boulons);
                toolTip.SetToolTip(boulons, upgradesDesc[i]);

                Button buttonBuy = new Button
                {
                    Text = "Acheter",
                    Tag = upgrades[i],
                    Location = new Point(260, 60 + i * 90),
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
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(450, 80),
                Size = new Size(380, 180)
            };
            panel.Controls.Add(groupBoxPassive);

            string[] passivePowers = { "PEUREUX", "CHANCEUX" };
            string[] passivePowersDesc = { "Quand un ennemi est dans votre champ de vision +10% de vitesse", "Vous avez 10% de chance d’avoir une pièce bonus quand vous en récupérez." };
            string[] passiveIcons = { "Bouclier.png", "Bouclier.png" }; // Replace with correct paths

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
                    Text = passivePowers[i],
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
                    Text = "5",
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
                    Tag = upgrades[i],
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
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(450, 280),
                Size = new Size(380, 200)
            };
            panel.Controls.Add(groupBoxActive);

            string[] activePowers = { "Bouclier", "Dégats", "Invisibilité" };
            string[] activePowersDesc = { "+1 cœur pendant 30 secondes", "Tuer les ennemis au toucher pendant 10 secondes", "Invisibilité pendant 10 secondes" };
            string[] activeIcons = { "Bouclier.png", "Bouclier.png", "Bouclier.png" }; // Replace with correct paths

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
                    Text = activePowers[i],
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = Color.White,
                    Location = new Point(60, 35 + i * 60),
                    Size = new Size(75, 20),
                    BackColor = Color.Transparent
                };
                groupBoxActive.Controls.Add(labelActive);
                toolTip.SetToolTip(labelActive, activePowersDesc[i]);


                Label labelPrice = new Label
                {
                    Text = "5",
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
                    Location = new Point(145, 35 + i * 60),
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
                    Location = new Point(260, 35 + i * 60),
                    Size = new Size(100, 30),
                    BackColor = Color.Blue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                buttonEquip.Click += new EventHandler(ButtonBuy_Click);
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            // Hide the panel and show the other elements
            panel.Visible = false;
            pictureBoxGarde.Visible = true;
            pictureBoxIngenieur.Visible = true;
            labelGarde.Visible = true;
            labelIngenieur.Visible = true;
        }

        private void ButtonBuy_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string item = button.Tag.ToString();
            MessageBox.Show("You have purchased: " + item);
        }
    }
}
