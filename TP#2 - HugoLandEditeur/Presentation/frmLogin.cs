using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HugoLand.Model;

namespace HugoLandEditeur.Presentation
{
    /// <summary>
    /// Auteur: Mathew Lemonde et Marc-André Landry
    /// Description : Cette form sert a authentifier un editeur de monde (admin)
    /// </summary>
    public partial class frmLogin : Form
    {
        private HugoLand.Controller.CompteJoueurController CompteController;
        private HugoLand.Model.HugoWorldEntities context;
        /// <summary>
        /// Auteur : Mathew Lemonde
        /// Description : Contructeur, doit recevoir le controller pour les compte 
        /// </summary>
        /// <param name="Controller">Controller serveur pour les compte</param>
        public frmLogin(HugoLand.Controller.CompteJoueurController Controller, HugoLand.Model.HugoWorldEntities db)
        {
            CompteController = Controller;
            context = db;
            //Marc Design
            InitializeComponent();
        }

        /// <summary>
        /// Auteur : Mathew Lemonde
        /// Click du bouton OK, vérifi le login et le statu du User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CompteController.ValidatePlayer(txtUserName.Text, txtPassword.Text))
            {

                var account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == txtUserName.Text);

                if (account.TypeUtilisateur == 1)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vous n'êtes pas admin", "Erreur", MessageBoxButtons.OK);
                }
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
        /// Auteur: Marc-André Landry
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
