using HugoWorldServiceRef;
using System;
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
    public partial class frmClass : Form
    {
        public frmClass()
        {
            InitializeComponent();
         
            List<Monde> lstMondes = HugoWorld.Data.MondeController.GetListMonde();
            if (lstMondes.Count == 0)
                DialogResult = DialogResult.Abort;

            foreach (Monde monde in lstMondes)
                comboBox1.Items.Add(monde.Description);

            comboBox1.DataSource = lstMondes;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Description";

            refreshDGV();
        }

        private void refreshDGV()
        {
            dgvClass.Rows.Clear();
            List<Classe> lstClass= HugoWorld.Data.ClassController.GetListClasses(GetMondeId());
            if (lstClass != null)
            {
                foreach (Classe classe in lstClass)
                    dgvClass.Rows.Add(classe.NomClasse, classe.Description, classe.Id);
            }
        }

        private int GetMondeId()
        {
            int value1 = 0;
            int.TryParse(comboBox1.SelectedValue.ToString(), out value1);
            return value1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshDGV();
        }

        private void dgvClass_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count == 1)
            {
                List<Classe> lstClass = HugoWorld.Data.ClassController.GetListClasses(GetMondeId());
                if (lstClass != null)
                {
                    Classe classe = lstClass[dgvClass.SelectedRows[0].Index];
                    txtNom.Text = classe.NomClasse;
                    txtDescription.Text = classe.Description;
                    txtDex.Text = classe.StatPoidsDex.ToString();
                    txtForce.Text = classe.StatPoidsStr.ToString();
                    txtIntelligence.Text = classe.StatPoidsInt.ToString();
                    txtStamina.Text = classe.StatPoidsStam.ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count == 1)
            {
                float dex, force, intel, stam;
                if (!String.IsNullOrEmpty(txtNom.Text) && 
                    !String.IsNullOrEmpty(txtDescription.Text) && 
                    !String.IsNullOrEmpty(txtDex.Text) && 
                    !String.IsNullOrEmpty(txtForce.Text) && 
                    !String.IsNullOrEmpty(txtIntelligence.Text) && 
                    !String.IsNullOrEmpty(txtStamina.Text) && 
                    float.TryParse(txtDex.Text, out dex) &&
                    float.TryParse(txtForce.Text, out force) &&
                    float.TryParse(txtIntelligence.Text, out intel) &&
                    float.TryParse(txtStamina.Text, out stam))
                {
                    HugoWorld.Data.ClassController.EditClassFromWorld(
                        int.Parse(dgvClass.SelectedRows[0].Cells[2].Value.ToString()),
                        txtNom.Text, txtDescription.Text, force, dex, intel, stam, GetMondeId());
                    refreshDGV();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count == 1 && MessageBox.Show(this, "Vous êtes sur le point de supprimer une classe\nRisque de perte d'héros\nVoullez vous continuer?", "Attention!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                HugoWorld.Data.ClassController.DeleteClass(int.Parse(dgvClass.SelectedRows[0].Cells[2].Value.ToString()));
                refreshDGV();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (new frmNewClass(GetMondeId()).ShowDialog() == DialogResult.OK)
                refreshDGV();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
