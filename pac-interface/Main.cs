using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Main()
        {
            InitializeComponent();
        }

        private void LoadPrincipal()
        {
            pnlCreation.Visible = false;
            pnlLancement.Visible = false;
            pnlProfil.Visible = false;
            btnBackProfile.Visible = false;
            btnBackLancement.Visible = false;
            btnBackNew.Visible = false;
            pnlPrincipal.Visible = true;
        }

        private void LoadProfile()
        {
            pnlLancement.Visible = false;
            pnlPrincipal.Visible = false;
            pnlCreation.Visible = false;
            btnBackLancement.Visible = false;
            btnBackNew.Visible = false;
            btnBackProfile.Visible = true;
            System.Threading.Thread.Sleep(10);
            pnlProfil.Visible = true;
        }

        private void LoadLancement()
        {
            LoadProfile();
            btnBackProfile.Visible = false;
            btnBackNew.Visible = false;
            btnBackLancement.Visible = true;
            pnlCreation.Visible = false;
            System.Threading.Thread.Sleep(10);
            pnlLancement.Visible = true;
        }

        private void LoadNew()
        {
            pnlProfil.Visible = false;
            pnlLancement.Visible = false;
            btnBackProfile.Visible = false;
            btnBackLancement.Visible = false;
            btnBackNew.Visible = true;
            System.Threading.Thread.Sleep(10);
            pnlCreation.Visible = true;
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
            profil = 1;
        }
        private void btnProfil2_Click(object sender, EventArgs e)
        {
            LoadLancement();
            profil = 2;
        }
        private void btnProfil3_Click(object sender, EventArgs e)
        {
            LoadLancement();
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

        private void lblPseudo_Click(object sender, EventArgs e)
        {

        }

        private void btnBackProfile_Click(object sender, EventArgs e)
        {
            LoadPrincipal();
        }

        private void btnBackLancement_Click(object sender, EventArgs e)
        {
            LoadProfile();
            profil = 0;
        }

        private void btnBackNew_Click(object sender, EventArgs e)
        {
            LoadLancement();
        }
    }
}
