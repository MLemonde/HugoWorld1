
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
        private int m_ID;

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public int[,] Tiles
        {
            get { return m_Tiles; }
            set { m_Tiles = value; }
        }

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

        /// <summary>
        /// Add tile to map and draw it
        /// </summary>
        /// <param name="xindex"></param>
        /// <param name="yindex"></param>
        /// <param name="TileID"></param>
        public void PlotTile(int xindex, int yindex, int TileID)
        {
            if (xindex < 0 || yindex < 0 || yindex >= m_Height || xindex >= m_Width)
                return;
            m_Tiles[yindex, xindex] = TileID;
            m_TileLibrary.DrawTile(m_BackBufferDC, TileID, xindex * csteApplication.TILE_WIDTH_IN_MAP, yindex * csteApplication.TILE_HEIGHT_IN_MAP);

        }

       

        public int Load()
        {
            
            
            m_BackBuffer = new Bitmap(m_Width * 16, m_Height * 16);
            m_BackBufferDC = Graphics.FromImage(m_BackBuffer);
            Refresh();

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