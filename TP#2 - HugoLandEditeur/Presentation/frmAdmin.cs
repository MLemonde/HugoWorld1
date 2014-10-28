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
    public partial class frmAdmin : Form
    {
        private HugoLand.Controller.CompteJoueurController CompteController;
        private HugoLand.Model.HugoWorldEntities context;

        /// <summary>
        /// Auteur : Mathew Lemonde
        /// Description : Contructeur, doit recevoir le controller pour les compte 
        /// </summary>
        /// <param name="Controller">Controller serveur pour les compte</param>        
        public frmAdmin(HugoLand.Controller.CompteJoueurController Controller, HugoLand.Model.HugoWorldEntities db)
        {
            CompteController = Controller;
            context = db;
            InitializeComponent();
        }

        
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!CompteController.CreatePlayer(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtFName.Text, txtLName.Text, 1))
                MessageBox.Show("Username already taken");
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
