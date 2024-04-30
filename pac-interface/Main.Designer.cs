namespace pac_interface
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            btnQuit = new Button();
            lblTitle = new Label();
            btnJouer = new Button();
            pnlPrincipal = new Panel();
            pnlProfil = new Panel();
            btnProfil3 = new Button();
            btnProfil2 = new Button();
            btnProfil1 = new Button();
            pnlLancement = new Panel();
            btnLancer = new Button();
            btnSupp = new Button();
            btnNew = new Button();
            pnlCreation = new Panel();
            lblPseudo = new Label();
            txtPseudo = new TextBox();
            btnValider = new Button();
            btnBackProfile = new Button();
            btnBackLancement = new Button();
            btnBackNew = new Button();
            pnlPrincipal.SuspendLayout();
            pnlProfil.SuspendLayout();
            pnlLancement.SuspendLayout();
            pnlCreation.SuspendLayout();
            SuspendLayout();
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top;
            btnQuit.BackColor = Color.Transparent;
            btnQuit.BackgroundImage = Properties.Resources.Btn;
            btnQuit.BackgroundImageLayout = ImageLayout.Zoom;
            btnQuit.Cursor = Cursors.Hand;
            btnQuit.FlatAppearance.BorderSize = 0;
            btnQuit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnQuit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnQuit.FlatStyle = FlatStyle.Flat;
            btnQuit.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnQuit.ForeColor = SystemColors.ButtonHighlight;
            btnQuit.Location = new Point(11, 138);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(214, 87);
            btnQuit.TabIndex = 1;
            btnQuit.Text = "Quitter";
            btnQuit.UseVisualStyleBackColor = false;
            btnQuit.Click += btnQuit_Click;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.CausesValidation = false;
            lblTitle.Font = new Font("Segoe UI", 42F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTitle.Location = new Point(498, 157);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(268, 74);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "PAC-BOT";
            // 
            // btnJouer
            // 
            btnJouer.Anchor = AnchorStyles.Top;
            btnJouer.BackColor = Color.Transparent;
            btnJouer.BackgroundImage = Properties.Resources.Btn;
            btnJouer.BackgroundImageLayout = ImageLayout.Zoom;
            btnJouer.Cursor = Cursors.Hand;
            btnJouer.FlatAppearance.BorderSize = 0;
            btnJouer.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnJouer.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnJouer.FlatStyle = FlatStyle.Flat;
            btnJouer.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnJouer.ForeColor = SystemColors.ButtonHighlight;
            btnJouer.Location = new Point(12, 12);
            btnJouer.Name = "btnJouer";
            btnJouer.Size = new Size(214, 87);
            btnJouer.TabIndex = 0;
            btnJouer.Text = "Jouer";
            btnJouer.UseVisualStyleBackColor = false;
            btnJouer.Click += btnJouer_Click;
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.Anchor = AnchorStyles.Top;
            pnlPrincipal.BackColor = Color.Transparent;
            pnlPrincipal.Controls.Add(btnQuit);
            pnlPrincipal.Controls.Add(btnJouer);
            pnlPrincipal.Location = new Point(511, 269);
            pnlPrincipal.Margin = new Padding(3, 2, 3, 2);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(236, 262);
            pnlPrincipal.TabIndex = 1;
            // 
            // pnlProfil
            // 
            pnlProfil.Anchor = AnchorStyles.Top;
            pnlProfil.BackColor = Color.Transparent;
            pnlProfil.Controls.Add(btnProfil3);
            pnlProfil.Controls.Add(btnProfil2);
            pnlProfil.Controls.Add(btnProfil1);
            pnlProfil.Location = new Point(258, 238);
            pnlProfil.Margin = new Padding(3, 2, 3, 2);
            pnlProfil.Name = "pnlProfil";
            pnlProfil.Size = new Size(236, 315);
            pnlProfil.TabIndex = 3;
            pnlProfil.Visible = false;
            // 
            // btnProfil3
            // 
            btnProfil3.Anchor = AnchorStyles.Top;
            btnProfil3.BackColor = Color.Transparent;
            btnProfil3.BackgroundImage = Properties.Resources.Btn;
            btnProfil3.BackgroundImageLayout = ImageLayout.Zoom;
            btnProfil3.Cursor = Cursors.Hand;
            btnProfil3.FlatAppearance.BorderSize = 0;
            btnProfil3.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnProfil3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnProfil3.FlatStyle = FlatStyle.Flat;
            btnProfil3.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnProfil3.ForeColor = SystemColors.ButtonHighlight;
            btnProfil3.Location = new Point(11, 214);
            btnProfil3.Name = "btnProfil3";
            btnProfil3.Size = new Size(214, 87);
            btnProfil3.TabIndex = 2;
            btnProfil3.Text = "Profil 3";
            btnProfil3.UseVisualStyleBackColor = false;
            btnProfil3.Click += btnProfil3_Click;
            // 
            // btnProfil2
            // 
            btnProfil2.Anchor = AnchorStyles.Top;
            btnProfil2.BackColor = Color.Transparent;
            btnProfil2.BackgroundImage = Properties.Resources.Btn;
            btnProfil2.BackgroundImageLayout = ImageLayout.Zoom;
            btnProfil2.Cursor = Cursors.Hand;
            btnProfil2.FlatAppearance.BorderSize = 0;
            btnProfil2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnProfil2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnProfil2.FlatStyle = FlatStyle.Flat;
            btnProfil2.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnProfil2.ForeColor = SystemColors.ButtonHighlight;
            btnProfil2.Location = new Point(11, 113);
            btnProfil2.Name = "btnProfil2";
            btnProfil2.Size = new Size(214, 87);
            btnProfil2.TabIndex = 1;
            btnProfil2.Text = "Profil 2";
            btnProfil2.UseVisualStyleBackColor = false;
            btnProfil2.Click += btnProfil2_Click;
            // 
            // btnProfil1
            // 
            btnProfil1.Anchor = AnchorStyles.Top;
            btnProfil1.BackColor = Color.Transparent;
            btnProfil1.BackgroundImage = Properties.Resources.Btn;
            btnProfil1.BackgroundImageLayout = ImageLayout.Zoom;
            btnProfil1.Cursor = Cursors.Hand;
            btnProfil1.FlatAppearance.BorderSize = 0;
            btnProfil1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnProfil1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnProfil1.FlatStyle = FlatStyle.Flat;
            btnProfil1.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnProfil1.ForeColor = SystemColors.ButtonHighlight;
            btnProfil1.Location = new Point(11, 12);
            btnProfil1.Name = "btnProfil1";
            btnProfil1.Size = new Size(214, 87);
            btnProfil1.TabIndex = 0;
            btnProfil1.Text = "Profil 1";
            btnProfil1.UseVisualStyleBackColor = false;
            btnProfil1.Click += btnProfil1_Click;
            // 
            // pnlLancement
            // 
            pnlLancement.Anchor = AnchorStyles.Top;
            pnlLancement.BackColor = Color.Transparent;
            pnlLancement.Controls.Add(btnLancer);
            pnlLancement.Controls.Add(btnSupp);
            pnlLancement.Controls.Add(btnNew);
            pnlLancement.Location = new Point(767, 238);
            pnlLancement.Margin = new Padding(3, 2, 3, 2);
            pnlLancement.Name = "pnlLancement";
            pnlLancement.Size = new Size(236, 315);
            pnlLancement.TabIndex = 2;
            pnlLancement.Visible = false;
            // 
            // btnLancer
            // 
            btnLancer.Anchor = AnchorStyles.Top;
            btnLancer.BackColor = Color.Transparent;
            btnLancer.BackgroundImage = Properties.Resources.Btn;
            btnLancer.BackgroundImageLayout = ImageLayout.Zoom;
            btnLancer.Cursor = Cursors.Hand;
            btnLancer.FlatAppearance.BorderSize = 0;
            btnLancer.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLancer.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLancer.FlatStyle = FlatStyle.Flat;
            btnLancer.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnLancer.ForeColor = SystemColors.ButtonHighlight;
            btnLancer.Location = new Point(10, 214);
            btnLancer.Name = "btnLancer";
            btnLancer.Size = new Size(214, 87);
            btnLancer.TabIndex = 2;
            btnLancer.Text = "Lancer";
            btnLancer.UseVisualStyleBackColor = false;
            // 
            // btnSupp
            // 
            btnSupp.Anchor = AnchorStyles.Top;
            btnSupp.BackColor = Color.Transparent;
            btnSupp.BackgroundImage = Properties.Resources.Btn;
            btnSupp.BackgroundImageLayout = ImageLayout.Zoom;
            btnSupp.Cursor = Cursors.Hand;
            btnSupp.FlatAppearance.BorderSize = 0;
            btnSupp.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSupp.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSupp.FlatStyle = FlatStyle.Flat;
            btnSupp.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            btnSupp.ForeColor = SystemColors.ButtonHighlight;
            btnSupp.Location = new Point(10, 113);
            btnSupp.Name = "btnSupp";
            btnSupp.Size = new Size(214, 87);
            btnSupp.TabIndex = 1;
            btnSupp.Text = " Supprimer";
            btnSupp.UseVisualStyleBackColor = false;
            btnSupp.Click += btnSupp_Click;
            // 
            // btnNew
            // 
            btnNew.Anchor = AnchorStyles.Top;
            btnNew.BackColor = Color.Transparent;
            btnNew.BackgroundImage = Properties.Resources.Btn;
            btnNew.BackgroundImageLayout = ImageLayout.Zoom;
            btnNew.Cursor = Cursors.Hand;
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnNew.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnNew.ForeColor = SystemColors.ButtonHighlight;
            btnNew.Location = new Point(10, 12);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(214, 87);
            btnNew.TabIndex = 0;
            btnNew.Text = " Nouveau";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // pnlCreation
            // 
            pnlCreation.Anchor = AnchorStyles.Top;
            pnlCreation.BackColor = Color.Transparent;
            pnlCreation.Controls.Add(lblPseudo);
            pnlCreation.Controls.Add(txtPseudo);
            pnlCreation.Controls.Add(btnValider);
            pnlCreation.Location = new Point(502, 238);
            pnlCreation.Margin = new Padding(3, 2, 3, 2);
            pnlCreation.Name = "pnlCreation";
            pnlCreation.Size = new Size(256, 315);
            pnlCreation.TabIndex = 4;
            pnlCreation.Visible = false;
            // 
            // lblPseudo
            // 
            lblPseudo.Anchor = AnchorStyles.Top;
            lblPseudo.AutoSize = true;
            lblPseudo.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            lblPseudo.ForeColor = Color.Gold;
            lblPseudo.Location = new Point(12, 32);
            lblPseudo.Name = "lblPseudo";
            lblPseudo.Size = new Size(231, 47);
            lblPseudo.TabIndex = 2;
            lblPseudo.Text = "Pseudonyme";
            lblPseudo.Click += lblPseudo_Click;
            // 
            // txtPseudo
            // 
            txtPseudo.Anchor = AnchorStyles.Top;
            txtPseudo.Cursor = Cursors.IBeam;
            txtPseudo.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            txtPseudo.Location = new Point(20, 92);
            txtPseudo.Margin = new Padding(3, 2, 3, 2);
            txtPseudo.MaxLength = 12;
            txtPseudo.Name = "txtPseudo";
            txtPseudo.Size = new Size(215, 53);
            txtPseudo.TabIndex = 0;
            // 
            // btnValider
            // 
            btnValider.Anchor = AnchorStyles.Top;
            btnValider.BackColor = Color.Transparent;
            btnValider.BackgroundImage = Properties.Resources.Btn;
            btnValider.BackgroundImageLayout = ImageLayout.Zoom;
            btnValider.Cursor = Cursors.Hand;
            btnValider.FlatAppearance.BorderSize = 0;
            btnValider.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnValider.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnValider.FlatStyle = FlatStyle.Flat;
            btnValider.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnValider.ForeColor = SystemColors.ButtonHighlight;
            btnValider.Location = new Point(21, 170);
            btnValider.Name = "btnValider";
            btnValider.Size = new Size(214, 87);
            btnValider.TabIndex = 1;
            btnValider.Text = "Valider";
            btnValider.UseVisualStyleBackColor = false;
            btnValider.Click += btnValider_Click;
            // 
            // btnBackProfile
            // 
            btnBackProfile.Anchor = AnchorStyles.Top;
            btnBackProfile.BackColor = Color.Transparent;
            btnBackProfile.BackgroundImage = (Image)resources.GetObject("btnBackProfile.BackgroundImage");
            btnBackProfile.BackgroundImageLayout = ImageLayout.Stretch;
            btnBackProfile.Cursor = Cursors.Hand;
            btnBackProfile.FlatAppearance.BorderSize = 0;
            btnBackProfile.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBackProfile.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBackProfile.FlatStyle = FlatStyle.Flat;
            btnBackProfile.Location = new Point(258, 126);
            btnBackProfile.Name = "btnBackProfile";
            btnBackProfile.Size = new Size(84, 38);
            btnBackProfile.TabIndex = 0;
            btnBackProfile.UseVisualStyleBackColor = false;
            btnBackProfile.Visible = false;
            btnBackProfile.Click += btnBackProfile_Click;
            // 
            // btnBackLancement
            // 
            btnBackLancement.Anchor = AnchorStyles.Top;
            btnBackLancement.BackColor = Color.Transparent;
            btnBackLancement.BackgroundImage = (Image)resources.GetObject("btnBackLancement.BackgroundImage");
            btnBackLancement.BackgroundImageLayout = ImageLayout.Stretch;
            btnBackLancement.Cursor = Cursors.Hand;
            btnBackLancement.FlatAppearance.BorderSize = 0;
            btnBackLancement.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBackLancement.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBackLancement.FlatStyle = FlatStyle.Flat;
            btnBackLancement.Location = new Point(258, 126);
            btnBackLancement.Name = "btnBackLancement";
            btnBackLancement.Size = new Size(84, 38);
            btnBackLancement.TabIndex = 6;
            btnBackLancement.UseVisualStyleBackColor = false;
            btnBackLancement.Visible = false;
            btnBackLancement.Click += btnBackLancement_Click;
            // 
            // btnBackNew
            // 
            btnBackNew.Anchor = AnchorStyles.Top;
            btnBackNew.BackColor = Color.Transparent;
            btnBackNew.BackgroundImage = (Image)resources.GetObject("btnBackNew.BackgroundImage");
            btnBackNew.BackgroundImageLayout = ImageLayout.Stretch;
            btnBackNew.Cursor = Cursors.Hand;
            btnBackNew.FlatAppearance.BorderSize = 0;
            btnBackNew.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBackNew.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBackNew.FlatStyle = FlatStyle.Flat;
            btnBackNew.Location = new Point(258, 126);
            btnBackNew.Name = "btnBackNew";
            btnBackNew.Size = new Size(84, 38);
            btnBackNew.TabIndex = 7;
            btnBackNew.UseVisualStyleBackColor = false;
            btnBackNew.Visible = false;
            btnBackNew.Click += btnBackNew_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.BGMenu;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnBackProfile);
            Controls.Add(pnlLancement);
            Controls.Add(pnlProfil);
            Controls.Add(lblTitle);
            Controls.Add(pnlPrincipal);
            Controls.Add(pnlCreation);
            Controls.Add(btnBackNew);
            Controls.Add(btnBackLancement);
            Name = "Main";
            Text = "Main";
            pnlPrincipal.ResumeLayout(false);
            pnlProfil.ResumeLayout(false);
            pnlLancement.ResumeLayout(false);
            pnlCreation.ResumeLayout(false);
            pnlCreation.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnQuit;
        private Label lblTitle;
        private Button btnJouer;
        private Panel pnlPrincipal;
        private Panel pnlProfil;
        private Button btnProfil3;
        private Button btnProfil2;
        private Button btnProfil1;
        private Panel pnlLancement;
        private Button btnLancer;
        private Button btnSupp;
        private Button btnNew;
        private Panel pnlCreation;
        private Button btnValider;
        private TextBox txtPseudo;
        private Label lblPseudo;
        private Button btnBackProfile;
        private Button btnBackLancement;
        private Button btnBackNew;
    }
}