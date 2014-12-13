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

            try
            {
                List<Hero> lstHeros = HugoWorld.Data.HeroController.GetListHero(HugoWorld.Data.UserId);
                foreach (var item in lstHeros)
                {
                    Classe cla = HugoWorld.Data.ClassController.FindClasseOfHero(item.Id, item.MondeId);
                    Monde mo = HugoWorld.Data.MondeController.GetListMonde().Where(c => c.Id == item.MondeId).FirstOrDefault();
                    dtgridViewHeros.Rows.Add(cla.NomClasse, mo.Description, item.Name, item.Experience, item.Id, item.ClasseId, item.MondeId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        private void btnEditClass_Click(object sender, EventArgs e)
        {
            new frmClass().ShowDialog(this);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (dtgridViewHeros.SelectedRows.Count == 1)
            {
                try
                {
                    HugoWorld.Data.HeroController.ConnectHero(HugoWorld.Data.CurrentHeroId);
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
            }
            else
                MessageBox.Show("Veuillez créer au moins un héro");
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

                HugoWorld.Data.WorldId = int.Parse(dtgridViewHeros.SelectedRows[0].Cells[6].Value.ToString());
                HugoWorld.Data.ClassId = int.Parse(dtgridViewHeros.SelectedRows[0].Cells[5].Value.ToString());
                HugoWorld.Data.CurrentHeroId = int.Parse(dtgridViewHeros.SelectedRows[0].Cells[4].Value.ToString());
                Hero herotolog = HugoWorld.Data.HeroController.GetListHero(HugoWorld.Data.UserId).FirstOrDefault(c => c.Id == HugoWorld.Data.CurrentHeroId);
                HugoWorld.Data.HeroName = herotolog.Name;
                HugoWorld.Data.Attack = herotolog.StatBaseStr + herotolog.StatBaseDex;
                HugoWorld.Data.Def = herotolog.StatBaseStr /2 + herotolog.StatBaseDex/2 + herotolog.StatBaseInt;
                HugoWorld.Data.Lvl = herotolog.Niveau;
                HugoWorld.Data.vie = herotolog.PV;
                HugoWorld.Data.Exp = (int)herotolog.Experience;
                HugoWorld.Data.Argent = (int)herotolog.Argent;
                HugoWorld.Data.Dex = herotolog.StatBaseDex;
                HugoWorld.Data.Intel = herotolog.StatBaseInt;
                HugoWorld.Data.Stam = herotolog.StatBaseStam;
                HugoWorld.Data.Str = herotolog.StatBaseStr;

                
                if (herotolog != default(Hero))
                {
                    txtStr.Text = herotolog.StatBaseStr.ToString();
                    txtDex.Text = herotolog.StatBaseDex.ToString();
                    txtIntelligence.Text = herotolog.StatBaseInt.ToString();
                    txtMoney.Text = herotolog.Argent.ToString();
                    txtStamina.Text = herotolog.StatBaseStam.ToString();
                }
            }
        }
    }
}
