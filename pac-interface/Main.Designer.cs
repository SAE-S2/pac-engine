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
            btnQuit = new Button();
            lblTitle = new Label();
            btnJouer = new Button();
            SuspendLayout();
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top;
            btnQuit.BackColor = Color.Transparent;
            btnQuit.BackgroundImage = Properties.Resources.Btn;
            btnQuit.BackgroundImageLayout = ImageLayout.Zoom;
            btnQuit.FlatAppearance.BorderSize = 0;
            btnQuit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnQuit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnQuit.FlatStyle = FlatStyle.Flat;
            btnQuit.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnQuit.ForeColor = SystemColors.ButtonHighlight;
            btnQuit.Location = new Point(525, 436);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(214, 87);
            btnQuit.TabIndex = 5;
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
            lblTitle.TabIndex = 3;
            lblTitle.Text = "PAC-BOT";
            // 
            // btnJouer
            // 
            btnJouer.Anchor = AnchorStyles.Top;
            btnJouer.BackColor = Color.Transparent;
            btnJouer.BackgroundImage = Properties.Resources.Btn;
            btnJouer.BackgroundImageLayout = ImageLayout.Zoom;
            btnJouer.FlatAppearance.BorderSize = 0;
            btnJouer.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnJouer.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnJouer.FlatStyle = FlatStyle.Flat;
            btnJouer.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnJouer.ForeColor = SystemColors.ButtonHighlight;
            btnJouer.Location = new Point(525, 283);
            btnJouer.Name = "btnJouer";
            btnJouer.Size = new Size(214, 87);
            btnJouer.TabIndex = 4;
            btnJouer.Text = "Jouer";
            btnJouer.UseVisualStyleBackColor = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.BGMenu;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnQuit);
            Controls.Add(lblTitle);
            Controls.Add(btnJouer);
            Name = "Main";
            Text = "Main";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnQuit;
        private Label lblTitle;
        private Button btnJouer;
    }
}