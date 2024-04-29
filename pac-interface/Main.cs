using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pac_interface
{

    public partial class Main : Form
    {
        int flag = 0;
        public Main()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnJouer_Click(object sender, EventArgs e)
        {
            pnlPrincipal.Visible = false;
            System.Threading.Thread.Sleep(100);
            pnlProfil.Visible = true;
        }

        private void btnProfil1_Click(object sender, EventArgs e)
        {
            pnlLancement.Visible = false;
            System.Threading.Thread.Sleep(100);
            pnlLancement.Visible = true;
            flag = 1;
        }
        private void btnProfil2_Click(object sender, EventArgs e)
        {
            pnlLancement.Visible = false;
            System.Threading.Thread.Sleep(100);
            pnlLancement.Visible = true;
            flag = 2;
        }
        private void btnProfil3_Click(object sender, EventArgs e)
        {
            pnlLancement.Visible = false;
            System.Threading.Thread.Sleep(100);
            pnlLancement.Visible = true;
            flag = 3;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            pnlProfil.Visible = false;
            pnlLancement.Visible = false;
            System.Threading.Thread.Sleep(100);
            pnlCreation.Visible = true;
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                btnProfil1.Text = txtPseudo.Text;
            }
            else if (flag == 2)
            {
                btnProfil2.Text = txtPseudo.Text;
            }
            else
            {
                btnProfil3.Text = txtPseudo.Text;
            }
            txtPseudo.Text = "";
            pnlCreation.Visible = false;
            System.Threading.Thread.Sleep(100);
            pnlProfil.Visible = true;
            pnlLancement.Visible = true;
        }

        private void btnSupp_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                btnProfil1.Text = "Profil 1";
            }
            else if (flag == 2)
            {
                btnProfil2.Text = "Profil 2";
            }
            else
            {
                btnProfil3.Text = "Profil 3";
            }
        }
    }
}
