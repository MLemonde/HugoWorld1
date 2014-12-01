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
    public partial class frmLogin : Form
    {
        public frmLogin()
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
            tryToConnect();
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

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                tryToConnect();
            }
        }

        #region methods

        private void tryToConnect()
        {
            CompteJoueurControllerClient s = new CompteJoueurControllerClient();

            if (s.ValidatePlayer(txtUserName.Text, txtPassword.Text))
            {

                HugoWorld.Data.userID = s.GetUserID(txtUserName.Text).Value;

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
        #endregion
    }
}
