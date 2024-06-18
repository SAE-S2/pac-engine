using pac_engine;
using PacDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pac_interface
{

    public partial class Main : Form
    {
        int profil = 0;
        int menu = 0;
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
    }
}
