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
            btnBack = new Button();
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
            btnQuit.Location = new Point(13, 184);
            btnQuit.Margin = new Padding(3, 4, 3, 4);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(245, 116);
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
            lblTitle.Location = new Point(569, 209);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(335, 93);
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
            btnJouer.Location = new Point(14, 16);
            btnJouer.Margin = new Padding(3, 4, 3, 4);
            btnJouer.Name = "btnJouer";
            btnJouer.Size = new Size(245, 116);
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
            pnlPrincipal.Location = new Point(584, 359);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(270, 349);
            pnlPrincipal.TabIndex = 1;
            // 
            // pnlProfil
            // 
            pnlProfil.Anchor = AnchorStyles.Top;
            pnlProfil.BackColor = Color.Transparent;
            pnlProfil.Controls.Add(btnProfil3);
            pnlProfil.Controls.Add(btnProfil2);
            pnlProfil.Controls.Add(btnProfil1);
            pnlProfil.Location = new Point(295, 317);
            pnlProfil.Name = "pnlProfil";
            pnlProfil.Size = new Size(270, 420);
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
            btnProfil3.Location = new Point(13, 285);
            btnProfil3.Margin = new Padding(3, 4, 3, 4);
            btnProfil3.Name = "btnProfil3";
            btnProfil3.Size = new Size(245, 116);
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
            btnProfil2.Location = new Point(13, 151);
            btnProfil2.Margin = new Padding(3, 4, 3, 4);
            btnProfil2.Name = "btnProfil2";
            btnProfil2.Size = new Size(245, 116);
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
            btnProfil1.Location = new Point(13, 16);
            btnProfil1.Margin = new Padding(3, 4, 3, 4);
            btnProfil1.Name = "btnProfil1";
            btnProfil1.Size = new Size(245, 116);
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
            pnlLancement.Location = new Point(877, 317);
            pnlLancement.Name = "pnlLancement";
            pnlLancement.Size = new Size(270, 420);
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
            btnLancer.Location = new Point(11, 285);
            btnLancer.Margin = new Padding(3, 4, 3, 4);
            btnLancer.Name = "btnLancer";
            btnLancer.Size = new Size(245, 116);
            btnLancer.TabIndex = 2;
            btnLancer.Text = "Lancer";
            btnLancer.UseVisualStyleBackColor = false;
            btnLancer.Click += btnLancer_Click;
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
            btnSupp.Location = new Point(11, 151);
            btnSupp.Margin = new Padding(3, 4, 3, 4);
            btnSupp.Name = "btnSupp";
            btnSupp.Size = new Size(245, 116);
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
            btnNew.Location = new Point(11, 16);
            btnNew.Margin = new Padding(3, 4, 3, 4);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(245, 116);
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
            pnlCreation.Location = new Point(574, 317);
            pnlCreation.Name = "pnlCreation";
            pnlCreation.Size = new Size(293, 420);
            pnlCreation.TabIndex = 4;
            pnlCreation.Visible = false;
            // 
            // lblPseudo
            // 
            lblPseudo.Anchor = AnchorStyles.Top;
            lblPseudo.AutoSize = true;
            lblPseudo.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            lblPseudo.ForeColor = Color.Gold;
            lblPseudo.Location = new Point(14, 43);
            lblPseudo.Name = "lblPseudo";
            lblPseudo.Size = new Size(291, 60);
            lblPseudo.TabIndex = 2;
            lblPseudo.Text = "Pseudonyme";
            // 
            // txtPseudo
            // 
            txtPseudo.Anchor = AnchorStyles.Top;
            txtPseudo.Cursor = Cursors.IBeam;
            txtPseudo.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            txtPseudo.Location = new Point(23, 123);
            txtPseudo.MaxLength = 12;
            txtPseudo.Name = "txtPseudo";
            txtPseudo.Size = new Size(245, 65);
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
            btnValider.Location = new Point(24, 227);
            btnValider.Margin = new Padding(3, 4, 3, 4);
            btnValider.Name = "btnValider";
            btnValider.Size = new Size(245, 116);
            btnValider.TabIndex = 1;
            btnValider.Text = "Valider";
            btnValider.UseVisualStyleBackColor = false;
            btnValider.Click += btnValider_Click;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top;
            btnBack.BackColor = Color.Transparent;
            btnBack.BackgroundImage = (Image)resources.GetObject("btnBack.BackgroundImage");
            btnBack.BackgroundImageLayout = ImageLayout.Stretch;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBack.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(295, 168);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(96, 51);
            btnBack.TabIndex = 6;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Visible = false;
            btnBack.Click += btnBack_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.BGMenu;
            CancelButton = btnBack;
            ClientSize = new Size(1445, 908);
            Controls.Add(pnlCreation);
            Controls.Add(pnlLancement);
            Controls.Add(pnlProfil);
            Controls.Add(lblTitle);
            Controls.Add(pnlPrincipal);
            Controls.Add(btnBack);
            Margin = new Padding(3, 4, 3, 4);
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
        private Button btnBack;
        private Button btnBackNew;
    }
}