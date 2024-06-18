using pac_engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private Panel Dialogues;
        private PictureBox Boite;
        private PictureBox Character;
        private Label DialogueText;
        private int numDialogue;
        private bool isFirstTime;

        public Main()
        {
            InitializeComponent();
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
            LoadNew();
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
            }
            else if (profil == 2)
            {
                btnProfil2.Font = txtfont;
                btnProfil2.Text = txtPseudo.Text;
            }
            else
            {
                btnProfil3.Font = txtfont;
                btnProfil3.Text = txtPseudo.Text;
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
            }
            else if (profil == 2)
            {
                btnProfil2.Font = new Font("Segoe UI", (float)26.5, FontStyle.Bold);
                btnProfil2.Text = "Profil 2";
            }
            else
            {
                btnProfil3.Font = new Font("Segoe UI", (float)26.5, FontStyle.Bold);
                btnProfil3.Text = "Profil 3";
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

        Game game;
        private void btnLancer_Click(object sender, EventArgs e)
        {
            PacBot test = new PacBot("test", 1280, 720);
            test.initializeGame();
            if (game == null)
            {
                game = new Game(test);
                this.Visible = false;
                game.Show();
                game.WindowState = FormWindowState.Maximized;
                game.FormClosed += Game_FormClosed;
            }
            else
            {
                game.Activate();
            }
            game.LoadMap();
            game.LoadEntities();
        }

        private void Game_FormClosed(object? sender, FormClosedEventArgs e)
        {
            game = null;
            this.Show();
        }
        private string WrapText(string text, int maxCharsPerLine) //Gestion du passage à la ligne
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

        private void Dialogue(int numDialogue, bool isFirstTime)
        {
            this.numDialogue = numDialogue;  // Initialisez les variables d'instance
            this.isFirstTime = isFirstTime;
            // Réinitialisation de currentLineIndex à 0
            currentLineIndex = 0;

            dialogueManager = new DialogueManager(numDialogue, isFirstTime);

            Dialogues = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Transparent
            };

            Boite = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("..\\..\\..\\Resources\\Bulle_dialogue.png"),
                Anchor = AnchorStyles.Bottom,
                Cursor = Cursors.Hand
            };

            Character = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            DialogueText = new Label
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

            Boite.Controls.Add(DialogueText);
            Controls.Add(Dialogues);
            Dialogues.Controls.Add(Boite);
            Dialogues.Controls.Add(Character);
            Character.BringToFront();
            Dialogues.BringToFront();

            DialogueText.Click += Dialogue_Click;

            AdjustSizesAndPositions();

            // Initial call to display the first dialogue line
            ShowCurrentDialogueLine(numDialogue, isFirstTime);

            Dialogues.Resize += (s, e) => AdjustSizesAndPositions();
        }

        private void Dialogue_Click(object sender, EventArgs e)
        {
            // Vérifie si le dialogue n'est pas terminé
            if (currentLineIndex < dialogueManager.GetDialogueLength(numDialogue, isFirstTime))
            {
                // Incrémentation de currentLineIndex
                currentLineIndex++;

                // Affichage de la ligne de dialogue suivante
                ShowCurrentDialogueLine(numDialogue, isFirstTime);
            }
            else
            {
                // Fin du dialogue, nettoyage
                Controls.Remove(Dialogues);
                Dialogues.Dispose();
            }
        }

        private void ShowCurrentDialogueLine(int numDialogue, bool isFirstTime)
        {
            var currentDialogue = dialogueManager.GetDialogueLine(numDialogue, isFirstTime, currentLineIndex);
            DialogueText.Text = WrapText(currentDialogue.Item1, 48);
            if (currentDialogue.Item2 == "Voix off")
            {

            }
            else
            {
                Character.Image = Image.FromFile($"..\\..\\..\\Resources\\Entity\\{currentDialogue.Item2}");
            }
        }


        private void AdjustSizesAndPositions()
        {
            Boite.Size = new Size((int)(Dialogues.ClientSize.Width * 0.8), (int)(Dialogues.ClientSize.Height * 0.5));
            Character.Size = new Size((int)(Dialogues.ClientSize.Width * 0.3), (int)(Dialogues.ClientSize.Height * 0.8));

            Character.Location = new Point(0, Dialogues.ClientSize.Height - Character.Height);
            Boite.Location = new Point(Dialogues.ClientSize.Width - Boite.Width, Dialogues.ClientSize.Height - Boite.Height);

            DialogueText.Size = new Size(Boite.Width - 20, Boite.Height - 20);
            DialogueText.Location = new Point((Boite.Width - DialogueText.Width) / 2, (Boite.Height - DialogueText.Height) / 2);
        }
    }
}
