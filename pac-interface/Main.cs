using pac_engine;
using pac_engine.Utils;
using PacDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pac_interface
{

    public partial class Main : Form
    {
        int profil = 0;
        int menu = 0;
        private int currentLineIndex = 0;
        private DialogueManager dialogueManager;
        private Panel dialoguesPanel;
        private PictureBox boite;
        private PictureBox character;
        private Label dialogueText;
        private int numDialogue;
        private bool isFirstTime;
        private bool dialogueInProgress;

        public Main()
        {
            InitializeComponent();
            DatabaseManager.AddUtilisateur("PacMaster", "1234");
            DatabaseManager.GetProfils();
            DatabaseManager.GetUtilisateurs();
        }

        private void LoadPrincipal()
        {
            pnlCreation.Visible = false;
            pnlLancement.Visible = false;
            pnlPrincipal.Visible = true;
            pnlProfil.Visible = false;
            btnBack.Visible = false;
            menu = 0;
        }

        private void LoadProfile()
        {
            pnlLancement.Visible = false;
            pnlPrincipal.Visible = false;
            pnlCreation.Visible = false;
            btnBack.Visible = true;
            pnlProfil.Visible = true;
            btnProfil1.ForeColor = Color.White;
            btnProfil2.ForeColor = Color.White;
            btnProfil3.ForeColor = Color.White;
            if (DatabaseManager.GetProfil_name(1, 1) != null)
                btnProfil1.Text = DatabaseManager.GetProfil_name(1, 1);
            if (DatabaseManager.GetProfil_name(1, 2) != null)
                btnProfil2.Text = DatabaseManager.GetProfil_name(1, 2);
            if (DatabaseManager.GetProfil_name(1, 3) != null)
                btnProfil3.Text = DatabaseManager.GetProfil_name(1, 3);

            switch (profil)
            {
                case 1:
                    btnProfil1.ForeColor = Color.Yellow;
                    break;
                case 2:
                    btnProfil2.ForeColor = Color.Yellow;
                    break;
                case 3:
                    btnProfil3.ForeColor = Color.Yellow;
                    break;
                default:
                    break;
            }
            menu = 1;
        }

        private void LoadLancement()
        {
            LoadProfile();
            btnBack.Visible = true;
            pnlCreation.Visible = false;
            pnlLancement.Visible = true;
            menu = 2;
        }

        private void LoadNew()
        {
            pnlProfil.Visible = false;
            pnlLancement.Visible = false;
            btnBack.Visible = true;
            pnlCreation.Visible = true;
            txtPseudo.Text = "";
            menu = 3;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnJouer_Click(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void btnProfil1_Click(object sender, EventArgs e)
        {
            LoadLancement();
            btnProfil1.ForeColor = Color.Yellow;
            btnProfil2.ForeColor = Color.White;
            btnProfil3.ForeColor = Color.White;
            profil = 1;
        }
        private void btnProfil2_Click(object sender, EventArgs e)
        {
            LoadLancement();
            btnProfil1.ForeColor = Color.White;
            btnProfil2.ForeColor = Color.Yellow;
            btnProfil3.ForeColor = Color.White;
            profil = 2;
        }
        private void btnProfil3_Click(object sender, EventArgs e)
        {
            LoadLancement();
            btnProfil1.ForeColor = Color.White;
            btnProfil2.ForeColor = Color.White;
            btnProfil3.ForeColor = Color.Yellow;
            profil = 3;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (profil)
            {
                case 1:
                    if (btnProfil1.Text == "Profil 1")
                        LoadNew();
                    else
                        MessageBox.Show("Profil déjà existant");
                    break;
                case 2:
                    if (btnProfil2.Text == "Profil 2")
                        LoadNew();
                    else
                        MessageBox.Show("Profil déjà existant");
                    break;
                case 3:
                    if (btnProfil3.Text == "Profil 3")
                        LoadNew();
                    else
                        MessageBox.Show("Profil déjà existant");
                    break;
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            Font txtfont;
            switch (txtPseudo.Text.Length) // Changement de la taille de la police
            {
                default:
                    txtfont = new Font("Segoe UI", (float)26.5, FontStyle.Bold);
                    break;
                case <= 8:
                    txtfont = new Font("Segoe UI", 20, FontStyle.Bold);
                    break;
                case <= 12:
                    txtfont = new Font("Segoe UI", 18, FontStyle.Bold);
                    break;
            }
            if (profil == 1)
            {
                btnProfil1.Font = txtfont;
                btnProfil1.Text = txtPseudo.Text;
                DatabaseManager.AddProfil(1, txtPseudo.Text, false, false, false, false, false, 0, 0, 1);
            }
            else if (profil == 2)
            {
                btnProfil2.Font = txtfont;
                btnProfil2.Text = txtPseudo.Text;
                DatabaseManager.AddProfil(2, txtPseudo.Text, false, false, false, false, false, 0, 0, 1);
            }
            else
            {
                btnProfil3.Font = txtfont;
                btnProfil3.Text = txtPseudo.Text;
                DatabaseManager.AddProfil(3, txtPseudo.Text, false, false, false, false, false, 0, 0, 1);
            }
            txtPseudo.Text = "";
            LoadLancement();
        }

        private void btnSupp_Click(object sender, EventArgs e)
        {
            if (profil == 1)
            {
                btnProfil1.Font = new Font("Segoe UI", (float)26.5, FontStyle.Bold);
                btnProfil1.Text = "Profil 1";
                DatabaseManager.DeleteProfil(1, 1);
            }
            else if (profil == 2)
            {
                btnProfil2.Font = new Font("Segoe UI", (float)26.5, FontStyle.Bold);
                btnProfil2.Text = "Profil 2";
                DatabaseManager.DeleteProfil(2, 1);
            }
            else
            {
                btnProfil3.Font = new Font("Segoe UI", (float)26.5, FontStyle.Bold);
                btnProfil3.Text = "Profil 3";
                DatabaseManager.DeleteProfil(3, 1);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (menu)
            {
                default:
                    break;
                case 1: //Menu profil actif
                    LoadPrincipal();
                    break;
                case 2: //Menu Lancement actif
                    profil = 0;
                    LoadProfile();
                    break;
                case 3: //Menu Pseudo actif
                    LoadLancement();
                    break;
            }
        }

        Hub hub;
        private void btnLancer_Click(object sender, EventArgs e)
        {
            switch (profil)
            {
                case 1:
                    if (btnProfil1.Text == "Profil 1")
                    {
                        MessageBox.Show("Profil non créé");
                        return;
                    }
                    break;
                case 2:
                    if (btnProfil2.Text == "Profil 2")
                    {
                        MessageBox.Show("Profil non créé");
                        return;
                    }
                    break;
                case 3:
                    if (btnProfil3.Text == "Profil 3")
                    {
                        MessageBox.Show("Profil non créé");
                        return;
                    }
                    break;
            }
            Globals.UID = 1;
            Globals.NumProfil = profil;
            hub = new Hub(null);
            this.Visible = false;
            hub.Show();
            hub.WindowState = FormWindowState.Maximized;
            hub.FormClosed += Hub_FormClosed;
        }

        private void Hub_FormClosed(object? sender, FormClosedEventArgs e)
        {
            hub = null;
            this.Show();
        }

        // MÃ©thode pour dÃ©marrer le dialogue
        private void StartDialogue(int numDialogue, bool isFirstTime)
        {
            dialogueInProgress = true; // Indicateur pour savoir si le dialogue est en cours
            currentLineIndex = 0;

            dialogueManager = new DialogueManager(numDialogue, isFirstTime);

            // Initialisation des contrÃ´les pour afficher le dialogue
            dialoguesPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Transparent
            };

            boite = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\Bulle_dialogue.png"),
                Anchor = AnchorStyles.Bottom,
                Cursor = Cursors.Hand
            };

            character = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            dialogueText = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                AutoEllipsis = true,
                Padding = new Padding(10),
                Enabled = true
            };

            boite.Controls.Add(dialogueText);
            Controls.Add(dialoguesPanel);
            dialoguesPanel.Controls.Add(boite);
            dialoguesPanel.Controls.Add(character);
            character.BringToFront();
            dialoguesPanel.BringToFront();

            dialogueText.Click += Dialogue_Click;

            AdjustSizesAndPositions();

            // Afficher la premiÃ¨re ligne de dialogue
            ShowCurrentDialogueLine(numDialogue, isFirstTime);

            // Boucle pour attendre la fin du dialogue
            while (dialogueInProgress)
            {
                Application.DoEvents(); // Permet Ã  l'application de traiter les Ã©vÃ©nements
                // Vous pouvez aussi utiliser Thread.Sleep(100) pour rÃ©duire l'utilisation du CPU
            }

            // Dialogue terminÃ©, nettoyage ou actions aprÃ¨s le dialogue
            Controls.Remove(dialoguesPanel);
            dialoguesPanel.Dispose();
        }

        // MÃ©thode pour afficher la ligne de dialogue courante
        private void ShowCurrentDialogueLine(int numDialogue, bool isFirstTime)
        {
            var currentDialogue = dialogueManager.GetDialogueLine(numDialogue, isFirstTime, currentLineIndex);
            dialogueText.Text = WrapText(currentDialogue.Item1, 48);

            if (currentDialogue.Item2 == "Voix off")
            {
                character.Image = null; // Aucune image pour "Voix off"
            }
            else
            {
                character.Image = Image.FromFile($"..\\..\\..\\Resources\\Entity\\{currentDialogue.Item2}");
            }

            // VÃ©rifier si c'est la derniÃ¨re ligne de dialogue
            if (currentLineIndex >= dialogueManager.GetDialogueLength(numDialogue, isFirstTime))
            {
                dialogueInProgress = false; // Fin du dialogue
            }
        }

        // MÃ©thode appelÃ©e lorsqu'un clic sur le dialogue se produit
        private void Dialogue_Click(object sender, EventArgs e)
        {
            // VÃ©rifier si le dialogue n'est pas terminÃ©
            if (dialogueInProgress && currentLineIndex < dialogueManager.GetDialogueLength(dialogueManager.dialogueIndex, dialogueManager.isFirstTime))
            {
                currentLineIndex++; // Passer Ã  la ligne suivante
                ShowCurrentDialogueLine(dialogueManager.dialogueIndex, dialogueManager.isFirstTime); // Afficher la nouvelle ligne
            }
        }

        // MÃ©thode pour ajuster les tailles et positions des contrÃ´les de dialogue
        private void AdjustSizesAndPositions()
        {
            boite.Size = new Size((int)(dialoguesPanel.ClientSize.Width * 0.8), (int)(dialoguesPanel.ClientSize.Height * 0.5));
            character.Size = new Size((int)(dialoguesPanel.ClientSize.Width * 0.3), (int)(dialoguesPanel.ClientSize.Height * 0.8));

            character.Location = new Point(0, dialoguesPanel.ClientSize.Height - character.Height);
            boite.Location = new Point(dialoguesPanel.ClientSize.Width - boite.Width, dialoguesPanel.ClientSize.Height - boite.Height);

            dialogueText.Size = new Size(boite.Width - 20, boite.Height - 20);
            dialogueText.Location = new Point((boite.Width - dialogueText.Width) / 2, (boite.Height - dialogueText.Height) / 2);
        }

        // MÃ©thode pour dÃ©couper le texte en lignes avec un nombre maximal de caractÃ¨res par ligne
        private string WrapText(string text, int maxCharsPerLine)
        {
            StringBuilder sb = new StringBuilder();
            string[] words = text.Split(' ');
            int currentLineLength = 0;

            foreach (string word in words)
            {
                if (currentLineLength + word.Length + 1 > maxCharsPerLine)
                {
                    sb.Append("\n" + word + " ");
                    currentLineLength = word.Length + 1;
                }
                else
                {
                    sb.Append(word + " ");
                    currentLineLength += word.Length + 1;
                }
            }

            return sb.ToString().Trim();
        }
    }
}
