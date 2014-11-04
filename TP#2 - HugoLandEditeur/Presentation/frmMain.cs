using HugoLand.Controller;
using HugoLand.Model;
using HugoLandEditeur.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HugoLandEditeur
{
    public partial class frmMain : Form
    {
        static HugoLand.Model.HugoWorldEntities context = new HugoLand.Model.HugoWorldEntities();
        private Monde CurrentWorld;
        MondeController mondeControleur = new MondeController(context);
        CompteJoueurController compteJoueurController = new CompteJoueurController(context);
        ClasseController classeController = new ClasseController(context);
        ObjetMondeController objetMondeController = new ObjetMondeController(context);
        MonstreController monstreController = new MonstreController(context);
        ItemController itemController = new ItemController(context);
        EffetItemController effetItemController = new EffetItemController(context);
        HeroController heroController = new HeroController(context);
        InventaireHeroController InventaireController = new InventaireHeroController(context);
        private CMap m_Map;
        private CTileLibrary m_TileLibrary;
        private int m_XSel;
        private int m_YSel;
        private int m_TilesHoriz;
        private System.Windows.Forms.Timer timer1;
        private int m_TilesVert;
        private bool m_bRefresh;
        private bool m_bResize;
        private int m_Zoom;
        private Rectangle m_TileRect;
        private Rectangle m_LibRect;
        private int m_ActiveXIndex;
        private int m_ActiveYIndex;
        private int m_ActiveTileID;
        private int m_ActiveTileXIndex;
        private int m_ActiveTileYIndex;
        private frmNew _frmnew;
        private int defaulttileid = 1;
        /// <summary>
        /// Summary description for Form1.
        /// </summary>
        /// 	
        public struct ComboItem
        {
            public string Text;
            public int Value;

            public ComboItem(string text, int val)
            {
                Text = text;
                Value = val;
            }
            public override string ToString()
            {
                return Text;
            }
        };

        public frmMain()
        {
            InitializeComponent();
        }

        /* -------------------------------------------------------------- *\
        frmMain_Load()			
        - Main Form Initialization		
    \* -------------------------------------------------------------- */
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            //LOGIN
         Presentation.frmLogin login = new Presentation.frmLogin(compteJoueurController, context);
         login.ShowDialog();
         if (login.DialogResult != System.Windows.Forms.DialogResult.OK)
             Application.Exit();




            m_Map = new CMap();
            m_TileLibrary = new CTileLibrary();
            m_Map.TileLibrary = m_TileLibrary;

            picMap.Parent = picEditArea;
            picMap.Left = 0;
            picMap.Top = 0;

            picTiles.Parent = picEditSel;
            picTiles.Width = m_TileLibrary.Width * csteApplication.TILE_WIDTH_IN_IMAGE;
            picTiles.Height = m_TileLibrary.Height * csteApplication.TILE_HEIGHT_IN_IMAGE;
            picTiles.Left = 0;
            picTiles.Top = 0;

            vscMap.Minimum = 0;
            vscMap.Maximum = m_Map.Height;
            m_YSel = 0;

            hscMap.Minimum = 0;
            hscMap.Maximum = m_Map.Width;
            m_XSel = 0;

            m_bRefresh = true;
            m_bResize = true;
            timer1.Enabled = true;
            m_Zoom = csteApplication.ZOOM;

            m_TileRect = new Rectangle(-1, -1, -1, -1);
            m_LibRect = new Rectangle(-1, -1, -1, -1);
            m_ActiveTileID = 32;

            //dlgLoadMap.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\maps\\";
            //dlgSaveMap.InitialDirectory = dlgLoadMap.InitialDirectory;
            m_bOpen = false;
            m_MenuLogic();
            //tmrLoad.Enabled = true;	

            m_pen = new Pen(Color.Orange, 4);
            m_brush = new SolidBrush(Color.FromArgb(160, 249, 174, 55));
            m_brush2 = new SolidBrush(Color.FromArgb(160, 255, 0, 0));

            m_bDrawTileRect = false;
            m_bDrawMapRect = false;

            comboBox1.Left = 220;
            comboBox1.Top = 2;
            comboBox1.Items.Add(new ComboItem("1X", 1));
            comboBox1.Items.Add(new ComboItem("2X", 2));
            comboBox1.Items.Add(new ComboItem("4X", 4));
            comboBox1.Items.Add(new ComboItem("8X", 8));
            comboBox1.Items.Add(new ComboItem("16X", 16));
            comboBox1.SelectedIndex = 1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            lblZoom.Left = 180;
            lblZoom.Top = 2;
            tbMain.Controls.Add(lblZoom);
            tbMain.Controls.Add(comboBox1);

        }


        /* -------------------------------------------------------------- *\
        Menus
    \* -------------------------------------------------------------- */
        #region Menu Code
        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Ouvre une fenêtre pour ajouter un nouveau admin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCreateNewUser_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin(compteJoueurController, context);
            f.ShowDialog(this);
        }

        private void mnuFileExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void mnuHelpAbout_Click(object sender, System.EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog(this);
        }

        private void mnuZoomX1_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 1;
            m_bResize = true;
        }

        private void mnuZoomX2_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 2;
            m_bResize = true;
        }

        private void mnuZoomX4_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 4;
            m_bResize = true;
        }

        private void mnuZoomX8_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 8;
            m_bResize = true;
        }

        private void mnuZoomX16_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 16;
            m_bResize = true;
        }

        private void mnuFileOpen_Click(object sender, System.EventArgs e)
        {
            LoadMap();
        }

        private void mnuFileSave_Click(object sender, System.EventArgs e)
        {
            m_SaveMap();
        }

        private void tbMain_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == tbbSave)
                m_SaveMap();
            if (e.Button == tbbOpen)
                LoadMap();
                if(e.Button == tbbHelp)
                {
                    frmAbout f = new frmAbout();
                    f.ShowDialog(this);
                }
            else if (e.Button == tbbNew)
                NewMap(true);
        }

        #endregion


        /* -------------------------------------------------------------- *\
            vscMap_Scroll()
            - vertical scroll bar for map editor window		
        \* -------------------------------------------------------------- */
        private void vscMap_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            m_YSel = e.NewValue;
            if (m_bOpen)
                picMap.Refresh();
        }

        /* -------------------------------------------------------------- *\
            hscMap_Scroll()
            - horizontal scroll bar for map editor window		
        \* -------------------------------------------------------------- */
        private void hscMap_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            m_XSel = e.NewValue;
            if (m_bOpen)
                picMap.Refresh();
        }

        /* -------------------------------------------------------------- *\
            picEditArea_Resize()
			
            - resize event for the parent of the map. The edit area is
              auto-sized to the space not taken by the lower and right 
              panes.		
        \* -------------------------------------------------------------- */
        private void picEditArea_Resize(object sender, System.EventArgs e)
        {
            if (m_bOpen)
            {
                m_XSel = 0;
                hscMap.Value = m_XSel;
                m_YSel = 0;
                vscMap.Value = m_YSel;
                m_bResize = true;
            }
        }

        /* -------------------------------------------------------------- *\
            timer1_Tick()
			
            - I'm not sure if this is necessary or not, but I was having
              difficulty updating things correctly due to timing of resizing
              items or updating scrolls and their values not getting set 
              until after the event already occurred... so I'm setting
              flags instead.
        \* -------------------------------------------------------------- */
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (m_bRefresh)
            {
                m_bRefresh = false;
                picMap.Refresh();
            }
            if (m_bResize)
            {
                m_bResize = false;
                m_ResizeMap();
            }
            if (m_bRefreshLib)
            {
                m_bRefreshLib = false;
                picTiles.Refresh();
            }
        }

        /* -------------------------------------------------------------- *\
            picMap_Paint()
			
            - This is where the Map picture box is painted to.
              This event happens when Refresh() is called or any section
              of the picture box is invalidated (i.e. covering up part of
              the picture box with another windows and then moving it away)
        \* -------------------------------------------------------------- */
        private void picMap_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_bOpen)
            {
                if (m_XSel < 0)
                    m_XSel = 0;
                if (m_YSel < 0)
                    m_YSel = 0;
                m_Map.Draw(e.Graphics, e.ClipRectangle, m_XSel, m_YSel);

                if (m_bDrawMapRect)
                    e.Graphics.FillRectangle(m_brush, m_TileRect);
            }
        }

        /* -------------------------------------------------------------- *\
            m_ResizeMap()
			
            - Takes care of Zoom, scroll and visible area logic.
        \* -------------------------------------------------------------- */
        private void m_ResizeMap()
        {
            int xpos, ypos;
            int nWidth = (vscMap.Left - 0); //picEditArea.Left);
            int AvailableWidth = nWidth - (2 * csteApplication.BUFFER_WIDTH);
            m_TilesHoriz = AvailableWidth / (m_Zoom * csteApplication.TILE_WIDTH_IN_MAP);
            int nMapWidth = m_TilesHoriz * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom;
            int BorderX = (nWidth - nMapWidth) / 2;

            int nHeight = (hscMap.Top - 0); //picEditArea.Top);
            int AvailableHeight = nHeight - 2 * csteApplication.BUFFER_HEIGHT;
            m_TilesVert = AvailableHeight / (m_Zoom * csteApplication.TILE_HEIGHT_IN_MAP);
            int nMapHeight = m_TilesVert * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom;
            int BorderY = (nHeight - nMapHeight) / 2;

            PrintDebug("AvailableHeight = " + AvailableHeight.ToString());
            PrintDebug("BorderY = " + BorderY.ToString());
            PrintDebug("AvailableWidth = " + AvailableWidth.ToString());
            PrintDebug("BorderX = " + BorderX.ToString());

            m_Map.OffsetX = 0; //BorderX;
            m_Map.OffsetY = 0; //BorderY;						
            m_Map.Zoom = m_Zoom;

            if (m_TilesHoriz < m_Map.Width)
            {
                //xpos = 16;
                xpos = 16 + (AvailableWidth - nMapWidth) / 2;
                m_Map.TilesHoriz = m_TilesHoriz;
                hscMap.Maximum = m_Map.Width - m_TilesHoriz;
            }
            else
            {
                m_Map.TilesHoriz = m_Map.Width;
                nMapWidth = m_Map.Width * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom;
                xpos = 16 + (AvailableWidth - nMapWidth) / 2;
                hscMap.Maximum = 0;
            }

            if (m_TilesVert < m_Map.Height)
            {
                //ypos = 32;
                ypos = 32 + (AvailableHeight - nMapHeight) / 2;
                m_Map.TilesVert = m_TilesVert;
                vscMap.Maximum = m_Map.Height - m_TilesVert;
            }
            else
            {
                m_Map.TilesVert = m_Map.Height;
                nMapHeight = m_Map.Height * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom;
                ypos = 32 + (AvailableHeight - nMapHeight) / 2;
                vscMap.Maximum = 0;
            }

            picMap.Location = new System.Drawing.Point(xpos, ypos);
            picMap.Size = new Size(nMapWidth, nMapHeight);

            m_bRefresh = true;
        }


        /* -------------------------------------------------------------- *\
            picMap_MouseMove()
			
            - Keeps track / translates coordinates to map tile to be
              updated if clicked on.
        \* -------------------------------------------------------------- */
        private void picMap_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0)
                return;
            if (e.X < m_TileRect.Left || e.X > m_TileRect.Right || e.Y < m_TileRect.Top || e.Y > m_TileRect.Bottom)
            {
                m_bDrawMapRect = true;

                m_Map.PointToTile(e.X, e.Y, ref m_ActiveXIndex, ref m_ActiveYIndex);
                m_Map.PointToBoundingRect(e.X, e.Y, ref m_TileRect);
                m_ActiveXIndex += m_XSel;
                m_ActiveYIndex += m_YSel;

                m_bRefresh = true;

                PrintDebug("XIndex = " + m_ActiveXIndex.ToString() + " YIndex = " + m_ActiveYIndex.ToString());
            }
        }

       /// <summary>
       /// 
       /// Dessine le tile et l'ajoute a la map
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void picMap_Click(object sender, System.EventArgs e)
        {        
            
            var test = m_TileLibrary.ObjMonde.Where(c => c.Value.X_Image + c.Value.Y_Image * 32 == m_ActiveTileID);            
            
            if(test.Count() == 0)
            {
                Console.WriteLine("non trouvé");
            }
            else
            {
                
                m_Map.PlotTile(m_ActiveXIndex, m_ActiveYIndex, m_ActiveTileID);
                m_bRefresh = true;
            }
            

        }

  

        /* -------------------------------------------------------------- *\
            picTiles_Paint()
			
            - Paints the tile library at the bottom of the screen.
        \* -------------------------------------------------------------- */
        private void picTiles_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_TileLibrary != null)
            {
                m_TileLibrary.Draw(e.Graphics, e.ClipRectangle);
                if (m_bDrawTileRect)
                    e.Graphics.FillRectangle(m_brush2, m_LibRect);
            }
        }

        /* -------------------------------------------------------------- *\
            picTiles_Click()
			
            - Selects the active tile ID
        \* -------------------------------------------------------------- */
        private void picTiles_Click(object sender, System.EventArgs e)
        {
            m_ActiveTileID = m_TileLibrary.TileToTileID(m_ActiveTileXIndex, m_ActiveTileYIndex);
            picActiveTile.Refresh();
        }

        /* -------------------------------------------------------------- *\
            vscTiles_Scroll()
			
            - controls the tile library scroll / position
        \* -------------------------------------------------------------- */
        private void vscTiles_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            picTiles.Top = -e.NewValue;
        }

        /* -------------------------------------------------------------- *\
            picTiles_MouseMove()
			
            - Keeps track / translates coordinates to tilelibrary tile to be
              selected if clicked on.
        \* -------------------------------------------------------------- */
        private void picTiles_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0)
                return;
            if (e.X < m_LibRect.Left || e.X > m_LibRect.Right || e.Y < m_LibRect.Top || e.Y > m_LibRect.Bottom)
            {
                m_bDrawTileRect = true;

                m_TileLibrary.PointToTile(e.X, e.Y, ref m_ActiveTileXIndex, ref m_ActiveTileYIndex);
                m_TileLibrary.PointToBoundingRect(e.X, e.Y, ref m_LibRect);

                m_bRefreshLib = true;

                PrintDebug("TileXIndex = " + m_ActiveTileXIndex.ToString() + " TileYIndex = " + m_ActiveTileYIndex.ToString());
                PrintDebug("X = " + e.X.ToString() + " Y = " + e.Y.ToString());
            }
        }

        /* -------------------------------------------------------------- *\
            ResetScroll()
			
            - Resets the scrollbar to 0.
              I'm not sure if this is necessary anymore.. I was trouble-
              shooting an odd issue.			  
        \* -------------------------------------------------------------- */
        private void ResetScroll()
        {
            vscMap.Value = 0;
            m_YSel = 0;
            hscMap.Value = 0;
            m_XSel = 0;
        }

        /* -------------------------------------------------------------- *\
            picActiveTile_Paint()
			
            - Displays the selected tile.	  
        \* -------------------------------------------------------------- */
        private void picActiveTile_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle destrect = new Rectangle(0, 0, picActiveTile.Width, picActiveTile.Height);
            m_TileLibrary.DrawTile(e.Graphics, m_ActiveTileID, destrect);
        }

        /* -------------------------------------------------------------- *\
            tmrLoad_Tick()
			
            - Loads the default map. 
        \* -------------------------------------------------------------- */
        private void tmrLoad_Tick(object sender, System.EventArgs e)
        {
            tmrLoad.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            m_Map.Refresh();
            m_bOpen = true;
            m_bRefresh = true;
            picMap.Visible = true;
            m_MenuLogic();
            this.Cursor = Cursors.Default;
        }
        #region Debug Code

        private void PrintDebug(String strDebug)
        {
            Console.WriteLine(strDebug);
        }
        #endregion

        /// <summary>
        /// 
        /// Ouvre un form, et load la map selectionnée
        /// </summary>
        private void LoadMap()
        {
            frmLoad frm = new frmLoad(mondeControleur, context);
            frm.ShowDialog(this);
            if (frm.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;

            List<Monde> mondelist = mondeControleur.GetListMonde();
            CurrentWorld = mondelist.Where(c => c.Id == frm.MondeID).First();

            m_Map.ID = frm.MondeID;

            this.Cursor = Cursors.WaitCursor;
            NewMap(false);

            for (int i = 0; i < m_Map.Height; i++)
                for (int j = 0; j < m_Map.Width; j++)
                {
                    

                    var Itemlist = CurrentWorld.Items.Where(c => c.x == j && c.y == i).ToList();
                    var ObjList = CurrentWorld.ObjetMondes.Where(c => c.x == j && c.y == i).ToList();
                    var MonsterList = CurrentWorld.Monstres.Where(c => c.x == j && c.y == i).ToList();

                   
                    if (ObjList.Count() != 0)
                    {
                        foreach (var ob in ObjList)
                        {
                            Tile tile = m_TileLibrary.ObjMonde[ob.Description];                            
                            m_Map.PlotTile(j,i, tile.X_Image + tile.Y_Image *32);
                            picMap.Refresh();

                          
                        }
                    }
                    if (Itemlist.Count() != 0)
                    {
                        foreach (var it in Itemlist)
                        {
                            Tile tile = m_TileLibrary.ObjMonde[it.Description];
                            m_Map.PlotTile(j, i, tile.X_Image + tile.Y_Image * 32);
                            picMap.Refresh();
                        }

                    }
                    if (MonsterList.Count() != 0)
                    {
                        foreach (var Mo in MonsterList)
                        {
                            Tile tile = m_TileLibrary.ObjMonde[Mo.Nom];
                            m_Map.PlotTile(j, i, tile.X_Image + tile.Y_Image * 32);
                            picMap.Refresh();


                        }
                    }

                }

            m_bOpen = true;
            picMap.Visible = true;           
            m_MenuLogic();

            m_ResizeMap();
            
            this.Cursor = Cursors.Default;






        }

       /// <summary>
       /// 
       /// Ajoute les objet de la map dans la BD
       /// </summary>
        private void m_SaveMap()
        {

            bool bnew;            
            Monde CurrentWorld = mondeControleur.GetListMonde().Find(c => c.Id == m_Map.ID);

            for (int i = 0; i < m_Map.Height; i++)
                for (int j = 0; j < m_Map.Width; j++)
                {
                    bnew = false;
                    var Itemlist = CurrentWorld.Items.Where(c => c.x == j && c.y == i).ToList();
                    var ObjList = CurrentWorld.ObjetMondes.Where(c => c.x == j && c.y == i).ToList();
                    var MonsterList = CurrentWorld.Monstres.Where(c => c.x == j && c.y == i).ToList();

                    var tile = m_TileLibrary.ObjMonde.Where(c => c.Value.X_Image + c.Value.Y_Image * 32 == m_Map.Tiles[i, j]).ToList();
                    foreach (var ti in tile)
                    {
                        switch (ti.Value.TypeObjet)
                        {
                            case TypeTile.ObjetMonde:
                                {
                                    if (ti.Value.Name != "Grass")
                                    {
                                        objetMondeController.CreateObjectMonde(j, i, ti.Value.Name, (int)TypeTile.ObjetMonde, m_Map.ID);
                                        bnew = true;
                                    }
                                    else
                                    {
                                        bnew = true;
                                    }
                                }
                                break;
                            case TypeTile.Monstre:
                                {
                                    monstreController.CreateMonster(m_Map.ID, j, i, ti.Value.Health, ti.Value.Name);
                                    bnew = true;
                                }
                                break;
                            case TypeTile.Item:
                                {
                                    itemController.CreateItem(m_Map.ID, j, i, ti.Value.Name, ti.Value.Name, 0, 1, 0, 0, 0, 0, 0, 0);
                                    bnew = true;
                                }
                                break;
                        }

                        if (bnew)
                        {
                            if (Itemlist.Count() != 0)
                            {
                                foreach (var it in Itemlist)
                                {

                                    itemController.DeleteItem(it.Id);
                                }
                            }
                            if (ObjList.Count() != 0)
                            {
                                int a = 0;

                                foreach (var ob in ObjList)
                                {
                                    var o1 = m_TileLibrary.ObjMonde[ob.Description];                                       

                                    if (a + 2 < ObjList.Count)
                                    {
                                        a++;
                                        objetMondeController.DeleteObjectMonde(ob.Id, m_Map.ID);
                                    }
                                    else
                                    {
                                        if (!o1.IsTransparent)
                                            objetMondeController.DeleteObjectMonde(ob.Id, m_Map.ID);

                                    }
                                }

                            }
                            if (MonsterList.Count() != 0)
                            {
                                foreach (var Mo in MonsterList)
                                    monstreController.DeleteMonster(Mo.Id);
                            }
                        }



                    }
                }
                          
        }

        /// <summary>
        /// Ouvre le form pour creer une map
        /// </summary>
        /// <returns></returns>
        private bool NewmapFrm()
        {
            DialogResult result;
            

            _frmnew = new frmNew();
            _frmnew.MapWidth = m_Map.Width;
            _frmnew.MapHeight = m_Map.Height;
            
            result = _frmnew.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                return true;
            }

            return false;
        }
       
        /// <summary>
        /// Creer une map dans le programme
        /// </summary>
        /// <param name="bnew">Nouvelle map (bd) ou load</param>
        private void NewMap(bool bnew)
        {
            bool bResult;

                m_bOpen = false;
                picMap.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                if(!bnew || NewmapFrm())
                {
                try
                {
                    if (!bnew)
                        bResult = m_Map.CreateNew(int.Parse(CurrentWorld.LimiteX), int.Parse(CurrentWorld.LimiteY), defaulttileid);
                    else
                        bResult = m_Map.CreateNew(_frmnew.Width, _frmnew.Height, defaulttileid);

                    if (bResult)
                    {

                        if(bnew)
                            AddMaptoModel(_frmnew.MapWidth, _frmnew.MapHeight, _frmnew.Desc);

                        m_bOpen = true;
                        m_bRefresh = true;
                        m_bResize = true;
                        picMap.Visible = true;
                    }
                }
                catch
                {
                    Console.WriteLine("Error Creating...");
                }
                m_MenuLogic();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        ///
        /// Permet de creer la map dans la BD
        /// </summary>
        /// <param name="x">Limite x</param>
        /// <param name="y">Limite y</param>
        /// <param name="sdesc">Description</param>
        private void AddMaptoModel(int x, int y,string sdesc)
        {
            mondeControleur.CreateMonde(x.ToString(), y.ToString(), sdesc);
           var mondelist =  mondeControleur.GetListMonde();
            m_Map.ID = mondelist.Last().Id;
            
        }

        /* -------------------------------------------------------------- *\
            m_MenuLogic()
			
            - Enables / Disables menus based on application status
        \* -------------------------------------------------------------- */
        private void m_MenuLogic()
        {
            bool bEnabled;

            bEnabled = m_bOpen;
            mnuFileSave.Enabled = bEnabled;
            mnuFileClose.Enabled = bEnabled;
            mnuZoom.Enabled = bEnabled;
            tbbSave.Enabled = bEnabled;
        }

        /* -------------------------------------------------------------- *\
            mnuFileNew_Click()
        \* -------------------------------------------------------------- */
        private void mnuFileNew_Click(object sender, System.EventArgs e)
        {
            NewMap(true);
        }

        private void picTiles_MouseLeave(object sender, System.EventArgs e)
        {
            m_bDrawTileRect = false;
            m_LibRect.X = -1;
            m_LibRect.Y = -1;
            m_LibRect.Width = -1;
            m_LibRect.Height = -1;
            m_bRefreshLib = true;
        }

        private void picMap_MouseLeave(object sender, System.EventArgs e)
        {
            m_bDrawMapRect = false;
            m_TileRect.X = -1;
            m_TileRect.Y = -1;
            m_TileRect.Width = -1;
            m_TileRect.Height = -1;
            m_bRefresh = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
            ComboItem myItem;
            myItem = (ComboItem)comboBox1.SelectedItem;
            ResetScroll();
            m_Zoom = myItem.Value;
            m_bResize = true;
            picTiles.Focus();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            picTiles.Left = -e.NewValue;
        }

       

       

       
    }
}
