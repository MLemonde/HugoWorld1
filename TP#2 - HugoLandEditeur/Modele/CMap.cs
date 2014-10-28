
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HugoLandEditeur
{
    /// <summary>
    /// Summary description for CMap.
    /// </summary>
    public class CMap
    {

        private int m_Width;			// map width (tiles)
        private int m_Height;			// map height (tiles)
        private int m_DefaultTileID;	// default tile id for outside normal bounds
        private int[,] m_Tiles;			// logical 2-D array for map building
        private Bitmap m_BackBuffer;		// Back Buffer for plotting graphical map data.. We will not store in picture box.
        private Graphics m_BackBufferDC;
        private int m_OffsetX;
        private int m_OffsetY;
        private int m_nTilesVert;
        private int m_nTilesHoriz;
        private int m_Zoom;



        private CTileLibrary m_TileLibrary;		// Reference to a Tile Library

        // Map Width (in Tiles)
        public int Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                m_Width = value;
            }
        }

        // Map Zoom (X)
        public int Zoom
        {
            get
            {
                return m_Zoom;
            }
            set
            {
                m_Zoom = value;
            }
        }

        // Map Height (in Tiles)
        public int Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                m_Height = value;
            }
        }
        // Default Tile ID
        public int DefaultTileID
        {
            get
            {
                return m_DefaultTileID;
            }
            set
            {
                m_DefaultTileID = value;
            }
        }

        // Default Tile ID
        public CTileLibrary TileLibrary
        {
            set
            {
                m_TileLibrary = value;
            }
        }

        // OffsetX (pixels)
        public int OffsetX
        {
            set
            {
                m_OffsetX = value;
            }
        }
        // OffsetY (pixels)
        public int OffsetY
        {
            set
            {
                m_OffsetY = value;
            }
        }

        // TilesVert
        public int TilesVert
        {
            set
            {
                m_nTilesVert = value;
            }
        }
        // TilesHoriz
        public int TilesHoriz
        {
            set
            {
                m_nTilesHoriz = value;
            }
        }

        public CMap()
        {

        }

        public void Refresh()
        {
            int i;
            int j;

            for (i = 0; i < m_Height; i++)
                for (j = 0; j < m_Width; j++)
                    m_TileLibrary.DrawTile(m_BackBufferDC, m_Tiles[i, j], j * csteApplication.TILE_WIDTH_IN_MAP, i * csteApplication.TILE_HEIGHT_IN_MAP);
        }

        public void Draw(Graphics pGraphics, Rectangle destRect, int TileX, int TileY)
        {
            int xindex = 0;
            int yindex = 0;
            int width;
            int height;

            width = destRect.Width / m_Zoom;
            height = destRect.Height / m_Zoom;

            PointToTile(destRect.X, destRect.Y, ref xindex, ref yindex);
            destRect.X = xindex * m_Zoom * csteApplication.TILE_WIDTH_IN_MAP;
            destRect.Y = yindex * m_Zoom * csteApplication.TILE_HEIGHT_IN_MAP;
            destRect.Width = (m_nTilesHoriz - xindex) * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom;
            destRect.Height = (m_nTilesVert - yindex) * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom;

            Rectangle srcRect = new Rectangle((TileX + xindex) * csteApplication.TILE_WIDTH_IN_MAP, (TileY + yindex) * csteApplication.TILE_HEIGHT_IN_MAP, (m_nTilesHoriz - xindex) * csteApplication.TILE_WIDTH_IN_MAP, (m_nTilesVert - yindex) * csteApplication.TILE_HEIGHT_IN_MAP);
            pGraphics.DrawImage(m_BackBuffer, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public void PointToTile(int x, int y, ref int xindex, ref int yindex)
        {
            // unscale zoom;
            x = x / m_Zoom;
            y = y / m_Zoom;

            xindex = x / csteApplication.TILE_WIDTH_IN_MAP;
            yindex = y / csteApplication.TILE_HEIGHT_IN_MAP;
        }

        public void PointToBoundingRect(int x, int y, ref Rectangle bounding)
        {
            x = x / m_Zoom;
            y = y / m_Zoom;
            x = x / csteApplication.TILE_WIDTH_IN_MAP;
            y = y / csteApplication.TILE_HEIGHT_IN_MAP;
            bounding.Size = new Size((csteApplication.TILE_WIDTH_IN_MAP * m_Zoom) + 6, (csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom) + 6);
            bounding.X = (x * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom) - 3;
            bounding.Y = (y * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom) - 3;
        }

        public void PlotTile(int xindex, int yindex, int TileID)
        {
            if (xindex < 0 || yindex < 0 || yindex >= m_Height || xindex >= m_Width)
                return;
            m_Tiles[yindex, xindex] = TileID;
            m_TileLibrary.DrawTile(m_BackBufferDC, TileID, xindex * csteApplication.TILE_WIDTH_IN_MAP, yindex * csteApplication.TILE_HEIGHT_IN_MAP);

        }

        public int Save(String strFilename)
        {
            //int i,j;

            //FileStream file = new FileStream(strFilename, FileMode.Create, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(file);

            //sw.WriteLine("ID: {0}",MAPFILE_ID.ToString());
            //sw.WriteLine("WIDTH: {0}",m_Width.ToString());
            //sw.WriteLine("HEIGHT: {0}",m_Height.ToString());
            //sw.WriteLine("DATA:");

            //for (i=0; i<m_Height; i++)
            //{
            //    for (j=0; j<m_Width; j++)
            //        sw.Write("{0},", m_Tiles[i,j]);
            //    sw.WriteLine();
            //}
            //sw.Close();

            return 0;
        }

        public int Load(String strFilename)
        {
            //int i;

            //FileStream file;						
            //StreamReader sr;
            //String strLine;
            //int index;
            //char[] delim = {':'};
            //char[] delim2 = {','};
            //int id = -1;
            //int width = -1;
            //int height = -1;
            //int data = -1;
            //String strVar;
            //String strValue;
            //String[] arrValues;
            //int count;
            //int[] arrData;
            //int rowcount = 0;

            //arrData = new int[128];

            //try
            //{
            //    file = new FileStream(strFilename, FileMode.Open, FileAccess.Read);
            //    sr = new StreamReader(file);
            //}
            //catch
            //{
            //    return -1;
            //}

            //while(sr.Peek() >= 0)
            //{					
            //    strLine = sr.ReadLine();
            //    index = strLine.IndexOfAny(delim);
            //    if (index > 0)
            //    {
            //        strVar = strLine.Substring(0,index);
            //        strVar = strVar.Trim();					
            //        strVar = strVar.ToLower();
            //        strValue = strLine.Substring(index+1);
            //        strValue = strValue.Trim();
            //        strValue = strValue.ToLower();

            //        if (strVar == "id")
            //            id = Convert.ToInt32(strValue);
            //        else if (strVar == "width")
            //            width = Convert.ToInt32(strValue);
            //        else if (strVar == "height")
            //            height = Convert.ToInt32(strValue);
            //        else if (strVar == "data")
            //        {
            //            data = 1;						
            //            break;
            //        }
            //    }
            //}

            //if (width <= 0 || height <=0 || data < 0 || id < 0)
            //    return -1;
            //if (width < 8 || width > MAP_MAX_WIDTH)
            //    return -1;
            //if (height < 8 || height > MAP_MAX_HEIGHT)
            //    return -1;

            //// Build Backbuffer
            //m_Width = width;
            //m_Height = height;
            //m_Tiles = new int[m_Height,m_Width];
            //m_BackBuffer = new Bitmap(m_Width * TILE_WIDTH_IN_MAP, m_Height * TILE_HEIGHT_IN_MAP);
            //m_BackBufferDC = Graphics.FromImage(m_BackBuffer);

            //while(sr.Peek() >= 0)
            //{
            //    strLine = sr.ReadLine();
            //    strLine = strLine.Trim();
            //    if (strLine.Length > 1)
            //    {
            //        arrValues = strLine.Split(delim2);

            //        count = 0;
            //        for (i=0; i<=arrValues.GetUpperBound(0); i++)
            //        {
            //            strValue = arrValues[i].Trim();
            //            if (strValue.Length > 0)
            //            {
            //                arrData[count] = Convert.ToByte(arrValues[i],10);
            //                count++;
            //            }
            //        }
            //        if (count != width)
            //            return -1;

            //        for (i=0; i<width; i++)
            //            m_Tiles[rowcount,i] = arrData[i];
            //        rowcount++;
            //    }
            //}			
            //sr.Close();

            //Refresh();

            return 0;
        }

        public bool CreateNew(int width, int height, int defaulttile)
        {
            int i, j;

            if (width < 8 || width > csteApplication.MAP_MAX_WIDTH)
                return false;
            if (height < 8 || height > csteApplication.MAP_MAX_HEIGHT)
                return false;

            // Build Backbuffer
            m_Width = width;
            m_Height = height;

            try
            {
                m_Tiles = new int[m_Height, m_Width];

                for (i = 0; i < m_Height; i++)
                    for (j = 0; j < m_Width; j++)
                        m_Tiles[i, j] = defaulttile;

                m_BackBuffer = new Bitmap(m_Width * csteApplication.TILE_WIDTH_IN_MAP, m_Height * csteApplication.TILE_HEIGHT_IN_MAP);
                m_BackBufferDC = Graphics.FromImage(m_BackBuffer);

                Refresh();
            }
            catch
            {
                return false;
            }
            return true;
        }


    }
}
