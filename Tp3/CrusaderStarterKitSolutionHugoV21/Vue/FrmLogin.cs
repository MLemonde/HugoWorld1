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
        bool _bEnterPressed = false;

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
            if (MessageBox.Show(this, "Voulez-vous vraiment quitter?", "Fermer?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                DialogResult = DialogResult.Abort;
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !_bEnterPressed)
            {
                tryToConnect();
                _bEnterPressed = true;
            }
            else
                _bEnterPressed = false;
        }

        private void tryToConnect()
        {
            if (HugoWorld.Data.CompteJoueurController.ValidatePlayer(txtUserName.Text, txtPassword.Text))
            {
                HugoWorld.Data.UserId = HugoWorld.Data.CompteJoueurController.GetUserID(txtUserName.Text).Value;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRegistEmail.Text) && !String.IsNullOrEmpty(txtRegistPass.Text) && !String.IsNullOrEmpty(txtRegistUsername.Text))
            {
                HugoWorld.Data.CompteJoueurController.CreatePlayer(txtRegistUsername.Text, txtPassword.Text, txtRegistEmail.Text, "user", "name", 0);
                txtUserName.Text = txtRegistUsername.Text;
                txtPassword.Text = txtRegistPass.Text;
                tryToConnect();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }
    }
}
