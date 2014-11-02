using System.Drawing;
namespace HugoLandEditeur
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.PictureBox picEditArea;
		private System.Windows.Forms.MainMenu mbMain;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.ToolBar tbMain;
		private System.Windows.Forms.Panel panelTools;
		private System.Windows.Forms.Panel panelTiles;
		private System.Windows.Forms.PictureBox picEditSel;
		private System.Windows.Forms.VScrollBar vscMap;
		private System.Windows.Forms.HScrollBar hscMap;
		private System.Windows.Forms.VScrollBar vscTiles;
		private System.Windows.Forms.PictureBox picTiles;
		private System.Windows.Forms.ImageList il16;


		private System.Windows.Forms.PictureBox picMap;
		private System.Windows.Forms.MenuItem mnuZoom;
		private System.Windows.Forms.MenuItem mnuZoomX1;
		private System.Windows.Forms.MenuItem mnuZoomX2;
		private System.Windows.Forms.MenuItem mnuZoomX4;
		private System.Windows.Forms.MenuItem mnuZoomX8;
		private System.Windows.Forms.MenuItem mnuZoomX16;
		private System.Windows.Forms.MenuItem mnuSettings;
		private System.Windows.Forms.ToolBarButton tbbNew;
		private System.Windows.Forms.ToolBarButton tbbOpen;
		private System.Windows.Forms.ToolBarButton tbbSave;
        private System.Windows.Forms.ToolBarButton tbbSeperator1;
		private System.Windows.Forms.ToolBarButton tbbHelp;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox picActiveTile;
		private System.Windows.Forms.Timer tmrLoad;
		private System.Windows.Forms.OpenFileDialog dlgLoadMap;
		private System.Windows.Forms.SaveFileDialog dlgSaveMap;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileClose;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mnuCreateNewUser;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem mnuFileSave;

		private bool						m_bOpen;
		private Pen							m_pen;
		private Brush						m_brush;
		private Brush						m_brush2;
		private bool						m_bRefreshLib;
		private bool						m_bDrawTileRect;
        private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label lblZoom;
		private bool						m_bDrawMapRect;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.picEditArea = new System.Windows.Forms.PictureBox();
            this.mbMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFileNew = new System.Windows.Forms.MenuItem();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileClose = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuFileSave = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuSettings = new System.Windows.Forms.MenuItem();
            this.mnuZoom = new System.Windows.Forms.MenuItem();
            this.mnuZoomX1 = new System.Windows.Forms.MenuItem();
            this.mnuZoomX2 = new System.Windows.Forms.MenuItem();
            this.mnuZoomX4 = new System.Windows.Forms.MenuItem();
            this.mnuZoomX8 = new System.Windows.Forms.MenuItem();
            this.mnuZoomX16 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuCreateNewUser = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.tbMain = new System.Windows.Forms.ToolBar();
            this.tbbNew = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbbSave = new System.Windows.Forms.ToolBarButton();
            this.tbbSeperator1 = new System.Windows.Forms.ToolBarButton();
            this.tbbHelp = new System.Windows.Forms.ToolBarButton();
            this.il16 = new System.Windows.Forms.ImageList(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panelTools = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.picActiveTile = new System.Windows.Forms.PictureBox();
            this.panelTiles = new System.Windows.Forms.Panel();
            this.picTiles = new System.Windows.Forms.PictureBox();
            this.vscTiles = new System.Windows.Forms.VScrollBar();
            this.picEditSel = new System.Windows.Forms.PictureBox();
            this.vscMap = new System.Windows.Forms.VScrollBar();
            this.hscMap = new System.Windows.Forms.HScrollBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picMap = new System.Windows.Forms.PictureBox();
            this.tmrLoad = new System.Windows.Forms.Timer(this.components);
            this.dlgLoadMap = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveMap = new System.Windows.Forms.SaveFileDialog();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblZoom = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.picEditArea)).BeginInit();
            this.panelTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picActiveTile)).BeginInit();
            this.panelTiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEditSel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // picEditArea
            // 
            this.picEditArea.BackColor = System.Drawing.Color.Gray;
            this.picEditArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEditArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picEditArea.Location = new System.Drawing.Point(0, 0);
            this.picEditArea.Name = "picEditArea";
            this.picEditArea.Size = new System.Drawing.Size(925, 528);
            this.picEditArea.TabIndex = 0;
            this.picEditArea.TabStop = false;
            this.picEditArea.Resize += new System.EventHandler(this.picEditArea_Resize);
            // 
            // mbMain
            // 
            this.mbMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuSettings,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileClose,
            this.menuItem4,
            this.mnuFileSave,
            this.menuItem6,
            this.mnuFileExit});
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileClose
            // 
            this.mnuFileClose.Index = 2;
            this.mnuFileClose.Text = "&Close";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "-";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Index = 4;
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 6;
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Index = 1;
            this.mnuSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuZoom,
            this.menuItem1,
            this.mnuCreateNewUser});
            this.mnuSettings.Text = "&Settings";
            // 
            // mnuZoom
            // 
            this.mnuZoom.Index = 0;
            this.mnuZoom.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuZoomX1,
            this.mnuZoomX2,
            this.mnuZoomX4,
            this.mnuZoomX8,
            this.mnuZoomX16});
            this.mnuZoom.Text = "&Zoom";
            // 
            // mnuZoomX1
            // 
            this.mnuZoomX1.Index = 0;
            this.mnuZoomX1.Text = "x 1";
            this.mnuZoomX1.Click += new System.EventHandler(this.mnuZoomX1_Click);
            // 
            // mnuZoomX2
            // 
            this.mnuZoomX2.Index = 1;
            this.mnuZoomX2.Text = "x 2";
            this.mnuZoomX2.Click += new System.EventHandler(this.mnuZoomX2_Click);
            // 
            // mnuZoomX4
            // 
            this.mnuZoomX4.Index = 2;
            this.mnuZoomX4.Text = "x 4";
            this.mnuZoomX4.Click += new System.EventHandler(this.mnuZoomX4_Click);
            // 
            // mnuZoomX8
            // 
            this.mnuZoomX8.Index = 3;
            this.mnuZoomX8.Text = "x 8";
            this.mnuZoomX8.Click += new System.EventHandler(this.mnuZoomX8_Click);
            // 
            // mnuZoomX16
            // 
            this.mnuZoomX16.Index = 4;
            this.mnuZoomX16.Text = "x 16";
            this.mnuZoomX16.Click += new System.EventHandler(this.mnuZoomX16_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // mnuCreateNewUser
            // 
            this.mnuCreateNewUser.Index = 2;
            this.mnuCreateNewUser.Text = "Create a new admin";
            this.mnuCreateNewUser.Click += new System.EventHandler(this.mnuCreateNewUser_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 2;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Index = 0;
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // tbMain
            // 
            this.tbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbNew,
            this.toolBarButton1,
            this.tbbOpen,
            this.tbbSave,
            this.tbbSeperator1,
            this.tbbHelp});
            this.tbMain.DropDownArrows = true;
            this.tbMain.ImageList = this.il16;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.ShowToolTips = true;
            this.tbMain.Size = new System.Drawing.Size(925, 28);
            this.tbMain.TabIndex = 1;
            this.tbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbMain_ButtonClick);
            // 
            // tbbNew
            // 
            this.tbbNew.ImageIndex = 2;
            this.tbbNew.Name = "tbbNew";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.toolBarButton1.Text = "-";
            // 
            // tbbOpen
            // 
            this.tbbOpen.ImageIndex = 0;
            this.tbbOpen.Name = "tbbOpen";
            // 
            // tbbSave
            // 
            this.tbbSave.ImageIndex = 1;
            this.tbbSave.Name = "tbbSave";
            // 
            // tbbSeperator1
            // 
            this.tbbSeperator1.Name = "tbbSeperator1";
            this.tbbSeperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.tbbSeperator1.Text = "-";
            // 
            // tbbHelp
            // 
            this.tbbHelp.ImageIndex = 3;
            this.tbbHelp.Name = "tbbHelp";
            // 
            // il16
            // 
            this.il16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il16.ImageStream")));
            this.il16.TransparentColor = System.Drawing.Color.Transparent;
            this.il16.Images.SetKeyName(0, "Open-icon.png");
            this.il16.Images.SetKeyName(1, "Save-icon.png");
            this.il16.Images.SetKeyName(2, "Files-New-File-icon.png");
            this.il16.Images.SetKeyName(3, "Help-icon.png");
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 506);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(925, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "sbMain";
            // 
            // panelTools
            // 
            this.panelTools.Controls.Add(this.label1);
            this.panelTools.Controls.Add(this.picActiveTile);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTools.Location = new System.Drawing.Point(837, 28);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(88, 478);
            this.panelTools.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Active Tile";
            // 
            // picActiveTile
            // 
            this.picActiveTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picActiveTile.Location = new System.Drawing.Point(8, 27);
            this.picActiveTile.Name = "picActiveTile";
            this.picActiveTile.Size = new System.Drawing.Size(64, 64);
            this.picActiveTile.TabIndex = 2;
            this.picActiveTile.TabStop = false;
            this.picActiveTile.Paint += new System.Windows.Forms.PaintEventHandler(this.picActiveTile_Paint);
            // 
            // panelTiles
            // 
            this.panelTiles.Controls.Add(this.hScrollBar1);
            this.panelTiles.Controls.Add(this.picTiles);
            this.panelTiles.Controls.Add(this.vscTiles);
            this.panelTiles.Controls.Add(this.picEditSel);
            this.panelTiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTiles.Location = new System.Drawing.Point(0, 372);
            this.panelTiles.Name = "panelTiles";
            this.panelTiles.Size = new System.Drawing.Size(837, 134);
            this.panelTiles.TabIndex = 4;
            // 
            // picTiles
            // 
            this.picTiles.BackColor = System.Drawing.Color.White;
            this.picTiles.Location = new System.Drawing.Point(16, 16);
            this.picTiles.Name = "picTiles";
            this.picTiles.Size = new System.Drawing.Size(392, 56);
            this.picTiles.TabIndex = 2;
            this.picTiles.TabStop = false;
            this.picTiles.Click += new System.EventHandler(this.picTiles_Click);
            this.picTiles.Paint += new System.Windows.Forms.PaintEventHandler(this.picTiles_Paint);
            this.picTiles.MouseLeave += new System.EventHandler(this.picTiles_MouseLeave);
            this.picTiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTiles_MouseMove);
            // 
            // vscTiles
            // 
            this.vscTiles.Dock = System.Windows.Forms.DockStyle.Right;
            this.vscTiles.LargeChange = 15;
            this.vscTiles.Location = new System.Drawing.Point(820, 0);
            this.vscTiles.Maximum = 1950;
            this.vscTiles.Name = "vscTiles";
            this.vscTiles.Size = new System.Drawing.Size(17, 134);
            this.vscTiles.SmallChange = 5;
            this.vscTiles.TabIndex = 10;
            this.vscTiles.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscTiles_Scroll);
            // 
            // picEditSel
            // 
            this.picEditSel.BackColor = System.Drawing.Color.White;
            this.picEditSel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEditSel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picEditSel.Location = new System.Drawing.Point(0, 0);
            this.picEditSel.Name = "picEditSel";
            this.picEditSel.Size = new System.Drawing.Size(837, 134);
            this.picEditSel.TabIndex = 0;
            this.picEditSel.TabStop = false;
            // 
            // vscMap
            // 
            this.vscMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.vscMap.LargeChange = 5;
            this.vscMap.Location = new System.Drawing.Point(820, 28);
            this.vscMap.Name = "vscMap";
            this.vscMap.Size = new System.Drawing.Size(17, 344);
            this.vscMap.TabIndex = 6;
            this.vscMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscMap_Scroll);
            // 
            // hscMap
            // 
            this.hscMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hscMap.LargeChange = 5;
            this.hscMap.Location = new System.Drawing.Point(0, 355);
            this.hscMap.Name = "hscMap";
            this.hscMap.Size = new System.Drawing.Size(820, 17);
            this.hscMap.TabIndex = 7;
            this.hscMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hscMap_Scroll);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picMap
            // 
            this.picMap.BackColor = System.Drawing.Color.White;
            this.picMap.Location = new System.Drawing.Point(16, 40);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(504, 304);
            this.picMap.TabIndex = 8;
            this.picMap.TabStop = false;
            this.picMap.Visible = false;
            this.picMap.Click += new System.EventHandler(this.picMap_Click);
            this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
            this.picMap.MouseLeave += new System.EventHandler(this.picMap_MouseLeave);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            // 
            // tmrLoad
            // 
            this.tmrLoad.Tick += new System.EventHandler(this.tmrLoad_Tick);
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(40, 328);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.TabStop = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblZoom
            // 
            this.lblZoom.Location = new System.Drawing.Point(176, 336);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(40, 24);
            this.lblZoom.TabIndex = 10;
            this.lblZoom.Text = "Zoom:";
            this.lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 116);
            this.hScrollBar1.Maximum = 800;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(820, 18);
            this.hScrollBar1.TabIndex = 11;
            this.hScrollBar1.Value = 100;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(925, 528);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.hscMap);
            this.Controls.Add(this.vscMap);
            this.Controls.Add(this.panelTiles);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.picEditArea);
            this.Menu = this.mbMain;
            this.Name = "frmMain";
            this.Text = "Éditeur de monde Hugo Land";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picEditArea)).EndInit();
            this.panelTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picActiveTile)).EndInit();
            this.panelTiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEditSel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}