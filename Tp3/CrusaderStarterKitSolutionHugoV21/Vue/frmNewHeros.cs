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
        MondeControllerClient _mondes = new MondeControllerClient();
        ClasseControllerClient _classes = new ClasseControllerClient();
        HeroControllerClient _Heros = new HeroControllerClient();


        public FrmCreateHeros()
        {
            InitializeComponent();
            cmbWorld.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClass.DropDownStyle = ComboBoxStyle.DropDownList;

            List<String> lstMonde = new List<String>();
            foreach (var item in _mondes.GetListMonde())
            {
                lstMonde.Add(item.Description);
            }
            cmbWorld.DataSource = lstMonde;
        }

        private void cmbWorld_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get current world ID to get the associated classes
            HugoWorld.Data.worldID = _mondes.GetWorldID(cmbWorld.SelectedItem.ToString()).Value;

            RefreshClasses(HugoWorld.Data.worldID);
        }


        #region methods

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
            foreach (var item in _classes.GetListClasses(HugoWorld.Data.worldID))
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

            dexterity = rmd.Next(1, maxStatForANoob);
            strength = rmd.Next(1, maxStatForANoob);
            stamina = rmd.Next(1, maxStatForANoob);
            intelligence = rmd.Next(1, maxStatForANoob);

            txtDex.Text = dexterity.ToString();
            txtStr.Text = strength.ToString();
            txtStamina.Text = stamina.ToString();
            txtIntelligence.Text = intelligence.ToString();
        }

        #endregion

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbClass.Items.Count >= 0)
                txtDescrptionClasse.Text = _classes.GetClassDescription(cmbClass.SelectedItem.ToString());

            HugoWorld.Data.ClassID = _classes.GetClassID(cmbClass.SelectedItem.ToString()).Value;

            GetRandomStats();
        }

        private void btnCreer_Click(object sender, EventArgs e)
        {
            //_Heros.CreateHero(HugoWorld.Data.worldID, HugoWorld.Data.userID, HugoWorld.Data.ClassID, 0, 0, 0,
            //    int.Parse(txtDex.Text), int.Parse(txtStr.Text), int.Parse(txtStamina.Text), int.Parse(txtIntelligence.Text), 0, 150);

            // Create an hero for malandry
            _Heros.CreateHero(HugoWorld.Data.worldID, 23, HugoWorld.Data.ClassID, 0, 0, 0,
                int.Parse(txtDex.Text), int.Parse(txtStr.Text), int.Parse(txtStamina.Text), int.Parse(txtIntelligence.Text), 0, 150);

            this.Close();
        }

        private void btnRegenerateStats_Click(object sender, EventArgs e)
        {
            GetRandomStats();
        }
    }
}
