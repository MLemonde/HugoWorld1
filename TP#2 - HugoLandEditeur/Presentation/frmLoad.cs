using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HugoLand;
using HugoLand.Model;

namespace HugoLandEditeur.Presentation
{
    public partial class frmLoad : Form
    {
        private HugoLand.Controller.MondeController _mondeController;
        private HugoLand.Model.HugoWorldEntities _context;
        public int MondeID { get; private set; }
        public frmLoad(HugoLand.Controller.MondeController mondeControll, HugoLand.Model.HugoWorldEntities context)
        {
            InitializeComponent();
            _context = context;
            _mondeController = mondeControll;

            List<Monde> lstmondes = _mondeController.GetListMonde();
            dataGridView1.DataSource = lstmondes;
        }

        /// <summary>
        /// Supprime le monde selectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
            _mondeController.DeleteMonde((int)dataGridView1.SelectedRows[0].Cells[0].Value);


            List<Monde> lstmondes = _mondeController.GetListMonde();
            dataGridView1.DataSource = lstmondes;
            }
        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Load le monde selectionner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count != 0)
                try
                {
                    MondeID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
            
              
            
        }
    }
}
