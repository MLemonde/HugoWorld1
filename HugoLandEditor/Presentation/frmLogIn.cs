using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HugoLandEditeur.Presentation
{
    public partial class frmLogIn : Form
    {
        string _userName1 = "Admin";
        string _password1 = "Admin";
        string _userName2 = "Admin";
        string _password2 = "Admin";
        bool _registrationOK = false;

        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == _userName1 && txtPassword.Text == _password1)
            {
                _registrationOK = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.",
                "Erreur",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        /// <summary>
        /// Auteur : MAL
        /// Description : Lorsqu'on ferme le form, s'assurer que l'utilisateur ne
        ///               se soit pas enregistrer, sinon ça ferme tout au complet!
        /// Date : 2014/10/21
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (_registrationOK)
                this.Dispose();
            else
            {
                // Confirm user wants to close
                switch (MessageBox.Show(this, "Voulez-vous vraiment quitter?", "Fermer?", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
