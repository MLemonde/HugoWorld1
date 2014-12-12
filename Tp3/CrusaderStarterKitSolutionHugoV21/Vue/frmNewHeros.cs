using HugoWorldServiceRef;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vue
{
    public partial class FrmCreateHeros : Form
    {
        public FrmCreateHeros()
        {
            InitializeComponent();
            cmbWorld.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClass.DropDownStyle = ComboBoxStyle.DropDownList;

            List<string> lstMondeString = new List<string>();
            List<Monde> lstMondes = HugoWorld.Data.MondeController.GetListMonde();
            foreach (Monde monde in lstMondes)
                lstMondeString.Add(monde.Description);
            cmbWorld.DataSource = lstMondeString;
        }

        private void cmbWorld_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get current world ID to get the associated classes
            HugoWorld.Data.WorldId = HugoWorld.Data.MondeController.GetWorldID(cmbWorld.SelectedItem.ToString()).Value;

            RefreshClasses(HugoWorld.Data.WorldId);
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// Description : Refresh du combobox pour les classes qui peuvent être 
        /// utilisées dans le monde le combobox n'a pas de méthode pour se mettre 
        /// à jour tout seul... On doit donc effacer le liste et la recommencer 
        /// à toute les fois.
        /// Merci C#!
        /// </summary>
        /// <param name="worldID">Le ID du monde</param>
        private void RefreshClasses(int worldID)
        {
            if(cmbClass.Items.Count != 0)
                cmbClass.Items.Clear();

            List<string> lstClass = new List<string>();
            List<Classe> classes = HugoWorld.Data.ClassController.GetListClasses(HugoWorld.Data.WorldId);
            foreach (var item in classes)
            {
                lstClass.Add(item.NomClasse);
                cmbClass.Items.Add(item.NomClasse);
            }

            if(cmbClass.Items.Count != 0)
                cmbClass.SelectedIndex = 0;
            cmbClass.Refresh();
        }

        /// <summary>
        /// On doit partager ici les stats du nouveau personnage de manière aléatoire
        /// entre la dextérité, la force, stamina et intelligence (4 stats)
        /// </summary>
        /// <param name="numberOfStatsToShare"></param>
        private void GetRandomStats()
        {
            int dexterity = 0, strength = 0, stamina = 0, intelligence = 0;
            int maxStatForANoob = 20;
            Random rmd = new Random();
            List<Classe> lstclass = HugoWorld.Data.ClassController.GetListClasses(HugoWorld.Data.WorldId);
            Classe heroclass = lstclass.FirstOrDefault(c => c.NomClasse == cmbClass.SelectedItem.ToString());
            dexterity = rmd.Next(1, maxStatForANoob) * (int)heroclass.StatPoidsDex;
            strength = rmd.Next(1, maxStatForANoob) * (int)heroclass.StatPoidsStr;
            stamina = rmd.Next(1, maxStatForANoob) * (int)heroclass.StatPoidsStam;
            intelligence = rmd.Next(1, maxStatForANoob) * (int)heroclass.StatPoidsInt;

            txtDex.Text = dexterity.ToString();
            txtStr.Text = strength.ToString();
            txtStamina.Text = stamina.ToString();
            txtIntelligence.Text = intelligence.ToString();
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbClass.Items.Count >= 0)
                txtDescrptionClasse.Text = HugoWorld.Data.ClassController.GetClassDescription(cmbClass.SelectedItem.ToString());

            HugoWorld.Data.ClassId = HugoWorld.Data.ClassController.GetClassID(cmbClass.SelectedItem.ToString()).Value;

            GetRandomStats();
        }

        private void btnCreer_Click(object sender, EventArgs e)
        {
            // Create an hero for connected user.
            HugoWorld.Data.HeroController.CreateHero(HugoWorld.Data.WorldId, HugoWorld.Data.UserId, HugoWorld.Data.ClassId,3, 3, 1,
                int.Parse(txtDex.Text), int.Parse(txtStr.Text), int.Parse(txtStamina.Text), int.Parse(txtIntelligence.Text),0, 150,txtHeroName.Text);

            DialogResult = DialogResult.OK;
        }

        private void btnRegenerateStats_Click(object sender, EventArgs e)
        {
            GetRandomStats();
        }
    }
}
