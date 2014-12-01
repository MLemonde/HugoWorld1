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
        HeroControllerClient _Heros = new HeroControllerClient();

        #region methods
        private void populateDataGridView()
        {

            List<Hero> lstHeros = new List<Hero>();
            foreach (var item in _Heros.GetListHero(HugoWorld.Data.userID))
            {
                dtgridViewHeros.Rows.Add(item.Classe, item.Monde, item.Niveau, item.Experience);
            }
        }
        #endregion

        public frmManage()
        {
            InitializeComponent();

            populateDataGridView();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Vue.FrmCreateHeros createHeroes = new Vue.FrmCreateHeros();
            createHeroes.ShowDialog();
        }


    }
}
