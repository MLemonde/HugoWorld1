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
        /// 
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
        /// 
        /// Click du bouton OK, vérifi le login et le statu du User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CompteController.ValidateAdmin(txtUserName.Text, txtPassword.Text))
            {               
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
