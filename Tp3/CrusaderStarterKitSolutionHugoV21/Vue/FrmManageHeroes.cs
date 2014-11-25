using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HugoWorldServiceRef;

namespace Vue
{
    public partial class FrmManageHeroes : Form
    {
        public FrmManageHeroes(int userID)
        {
            InitializeComponent();

            HeroControllerClient s = new HeroControllerClient();
            List<Hero> lstHero = s.GetListHero(userID);

        }
    }
}
