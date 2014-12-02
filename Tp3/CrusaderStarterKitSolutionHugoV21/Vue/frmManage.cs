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
    public partial class frmManage : Form
    {
        public frmManage()
        {
            InitializeComponent();

            refreshDataGridView();

            //si le user est Admin!
            //il peut manage les classes
            if (HugoWorld.Data.CompteJoueurController.ValidateAdmin2(HugoWorld.Data.UserId))
                btnEditClass.Visible = true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (new Vue.FrmCreateHeros().ShowDialog() == DialogResult.OK)
                refreshDataGridView();
        }

        private void refreshDataGridView()
        {
            dtgridViewHeros.Rows.Clear();

            List<Hero> lstHeros = HugoWorld.Data.HeroController.GetListHero(HugoWorld.Data.UserId);
            foreach (var item in lstHeros)
                dtgridViewHeros.Rows.Add(item.Classe.Description, item.Monde.Description, item.Niveau, item.Experience, item.Id);
        }

        private void btnEditClass_Click(object sender, EventArgs e)
        {
            new frmClass().ShowDialog(this);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (dtgridViewHeros.SelectedRows.Count == 1)
                DialogResult = DialogResult.OK;
            else
            {
                MessageBox.Show("Veuillez créer au moins un héro");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtgridViewHeros.SelectedRows.Count == 1)
            {
                HugoWorld.Data.HeroController.DeleteHero(int.Parse(dtgridViewHeros.SelectedRows[0].Cells[4].Value.ToString()));
                refreshDataGridView();
            }
        }

        private void dtgridViewHeros_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgridViewHeros.SelectedRows.Count == 1)
            {
                HugoWorld.Data.CurrentHeroId = int.Parse(dtgridViewHeros.SelectedRows[0].Cells[4].Value.ToString());
                Hero hero = HugoWorld.Data.HeroController.GetListHero(HugoWorld.Data.UserId).FirstOrDefault(h => h.Id == HugoWorld.Data.CurrentHeroId);
                if (hero != default(Hero))
                {
                    txtStr.Text = hero.StatBaseStr.ToString();
                    txtDex.Text = hero.StatBaseDex.ToString();
                    txtIntelligence.Text = hero.StatBaseInt.ToString();
                    txtMoney.Text = hero.Argent.ToString();
                    txtStamina.Text = hero.StatBaseStam.ToString();
                }
            }
        }
    }
}
