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
    public partial class frmNewClass : Form
    {
        int _mondeId;
        public frmNewClass(int mondeId)
        {
            InitializeComponent();
            _mondeId = mondeId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void btnCreate_Click(object sender, EventArgs e)
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
                HugoWorld.Data.ClassController.CreateClass(txtNom.Text, txtDescription.Text, force, dex, intel, stam, _mondeId);
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(this, "Un des champs est vide ou la dex, force, intel, stamina n'est pas valide.");
        }
    }
}
