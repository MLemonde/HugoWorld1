using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HugoWorldServiceRef;

namespace Vue
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// Click du bouton OK, vérifi le login et le statu du User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            CompteJoueurControllerClient s = new CompteJoueurControllerClient();

            if (s.ValidatePlayer(txtUserName.Text, txtPassword.Text))
            {

                HugoWorld.Data.userID = s.GetUserID(txtUserName.Text, txtPassword.Text).Value;

                DialogResult = System.Windows.Forms.DialogResult.OK;
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

        /// <summary>
        /// 
        /// Description : Ferme l'application au click du bouton fermer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermer_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Voulez-vous vraiment quitter?", "Fermer?", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    this.Dispose();
                    break;
                default:
                    break;
            }


        }
    }
}
