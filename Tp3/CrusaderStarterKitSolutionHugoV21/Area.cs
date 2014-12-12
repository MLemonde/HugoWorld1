using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using HugoWorldServiceRef;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HugoWorld
{

    /// <summary>
    /// Area defines the 8x8 grid that contains a set of MapTiles
    /// </summary>
    public class Area : GameObject
    {
        public const int AreaOffsetX = 30;
        public const int AreaOffsetY = 50;
        public const int MapSizeX = 8;
        public const int MapSizeY = 8;
        private Monde currentWorld;
        private int _x;
        private int _y;
        Dictionary<string, Tile> _tiles;
        public MapTile[,] Map = new MapTile[MapSizeX, MapSizeY];
        public MapTile[,] MapItem = new MapTile[MapSizeX, MapSizeY];
        private Rectangle _areaRectangle = new Rectangle(AreaOffsetX, AreaOffsetY, MapSizeX * Tile.TileSizeX, MapSizeY * Tile.TileSizeY);

        public string Name;
        public string NorthArea;
        public string EastArea;
        public string SouthArea;
        public string WestArea;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mondeid"></param>
        /// <param name="x">Position du héro X</param>
        /// <param name="y">Position du héro Y</param>
        /// <param name="tiles"></param>
        public Area(int Mondeid, int x, int y, Dictionary<string, Tile> tiles)
        {
            _x = x;
            _y = y;
            _tiles = tiles;
            FillGrass();
            MondeControllerClient client = new MondeControllerClient();
            List<Monde> lstmonde = client.GetListMonde();
            currentWorld = lstmonde.FirstOrDefault(c => c.Id == Mondeid);

            Name = x.ToString() + ',' + y.ToString();
            if (y == 0)
                NorthArea = "-";
            else
                NorthArea = x.ToString() + ',' + (y - 1).ToString();

            if (x * 8 + 8 == int.Parse(currentWorld.LimiteX))
                EastArea = "-";
            else
                EastArea = (x + 1).ToString() + ',' + (y).ToString();

            if (y * 8 + 8 == int.Parse(currentWorld.LimiteY))
                SouthArea = "-";
            else
                SouthArea = x.ToString() + ',' + (y + 1).ToString();

            if (x == 0)
                WestArea = "-";
            else
                WestArea = (x - 1).ToString() + ',' + (y).ToString();


            if (currentWorld == null)
                return;
            int Mondex = int.Parse(currentWorld.LimiteX) / 8;
            int Mondey = int.Parse(currentWorld.LimiteY) / 8;



            //Read in 8 lines of 8 characters each. Look up the tile and make the
            //matching sprite

            Refresh();





            ////Read game objects until the blank line
            //while (!stream.EndOfStream && (line = stream.ReadLine().Trim()) != "")
            //{
            //    //Each line is an x,y coordinate and a tile shortcut
            //    //Look up the tile and construct the sprite
            //    string[] elements = line.Split(',');
            //    int x = Convert.ToInt32(elements[0]);
            //    int y = Convert.ToInt32(elements[1]);
            //    MapTile mapTile = Map[x, y];
            //    mapTile.ObjectTile = tiles[elements[2]];
            //    mapTile.SetObjectSprite(x, y);

            //    if (mapTile.ObjectTile.IsTransparent)
            //    {
            //        mapTile.ObjectSprite.ColorKey = Color.FromArgb(75, 75, 75);
            //    }
            //}

        }

        public void Refresh()
        {
            List<Monstre> lstmonstre = currentWorld.Monstres.Where(c => c.MondeId == currentWorld.Id
                && c.x >= _x * MapSizeX
                && c.x < (_x * MapSizeX + MapSizeX)
                && c.y >= _y * MapSizeY
                && c.y < (_y * MapSizeY + MapSizeY)).ToList();
            List<ObjetMonde> lstobj = currentWorld.ObjetMondes.Where(c => c.MondeId == currentWorld.Id
                && c.x >= _x * MapSizeX
                && c.x < (_x * MapSizeX + MapSizeX)
                && c.y >= _y * MapSizeY
                && c.y < (_y * MapSizeY + MapSizeY)).ToList();
            List<Item> lstitem = currentWorld.Items.Where(c => c.MondeId == currentWorld.Id
                && c.x >= _x * MapSizeX
                && c.x < (_x * MapSizeX + MapSizeX)
                && c.y >= _y * MapSizeY
                && c.y < (_y * MapSizeY + MapSizeY)).ToList();

            foreach (var m in lstmonstre)
            {
                int xm = m.x - _x * MapSizeX;
                int ym = m.y - _y * MapSizeY;

                MapTile mapTile = new MapTile();
                Map[xm, ym] = mapTile;
                mapTile.Tile = _tiles[m.Nom];
                mapTile.ObjectTile = _tiles[m.Nom];
                mapTile.SetSprite(xm, ym);
            }
            foreach (var m in lstobj)
            {
                int xo = m.x - _x * MapSizeX;
                int yo = m.y - _y * MapSizeY;

                if (m.Description != "Sand" && m.Description != "Snow"
                    && m.Description != "Grass" && m.Description != "Dirt"
                    && m.Description != "Path4way"
                    && m.Description != "PathH" && m.Description != "PathV" && m.Description != "PathCornerUR")
                {
                    MapTile mapTile = new MapTile();
                    Map[xo, yo] = mapTile;
                    mapTile.Tile = _tiles[m.Description];
                    mapTile.ObjectTile = _tiles[m.Description];
                    mapTile.SetSprite(xo, yo);
                }
                else
                {

                    MapTile mapTile = new MapTile();
                    MapItem[xo, yo] = mapTile;
                    mapTile.Tile = _tiles[m.Description];
                    mapTile.SetSprite(xo, yo);
                }

            }
            foreach (var m in lstitem)
            {
                int xi = m.x.Value - _x * MapSizeX;
                int yi = m.y.Value - _y * MapSizeY;

                MapTile mapTile = new MapTile();
                Map[xi, yi] = mapTile;
                mapTile.Tile = _tiles[m.Nom];
                mapTile.ObjectTile = _tiles[m.Nom];
                mapTile.SetSprite(xi, yi);
            }

            //for (int j = 0; j < MapSizeY; j++)
            //{
            //    for (int i = 0; i < MapSizeX; i++)
            //    {


            //        MapTile mapTile2 = new MapTile();
            //        if (MapItem[i, j] == null)
            //        {
            //            MapItem[i, j] = mapTile2;
            //            mapTile2.Tile = _tiles["Grass"];
            //            mapTile2.SetSprite(i, j);
            //        }

            //        MapTile mapTile = new MapTile();
            //        if (Map[i, j] == null)
            //        {
            //            Map[i, j] = MapItem[i, j];

            //        }
            //    }

            //}
        }

        public void FillGrass()
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                for (int i = 0; i < MapSizeX; i++)
                {


                    MapTile mapTile2 = new MapTile();
                    
                        MapItem[i, j] = mapTile2;
                        mapTile2.Tile = _tiles["Grass"];
                        mapTile2.SetSprite(i, j);
                    

                    
                      Map[i, j] = MapItem[i, j];

                    
                }

            }
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            //Update all the map tiles and any objects
            foreach (MapTile mapTile in Map)
            {
                if (mapTile != null)
                    mapTile.Sprite.Update(gameTime, elapsedTime);
                //if (mapTile.ObjectSprite != null)
                //{
                //    if (mapTile.ObjectSprite.NumberOfFrames > 1)
                //    {
                //        mapTile.ObjectSprite.CurrentFrame = (int)((gameTime * 8.0) % (double)mapTile.ObjectSprite.NumberOfFrames);
                //    }
                //    mapTile.ObjectSprite.Update(gameTime, elapsedTime);
                //}
            }

            foreach (MapTile mapTile in MapItem)
            {
                mapTile.Sprite.Update(gameTime, elapsedTime);
            }
        }

        public override void Draw(Graphics graphics)
        {
            foreach (MapTile mapTile in MapItem)
            {
                if (mapTile != null)
                {
                    if (mapTile.Sprite != null)
                        mapTile.Sprite.Draw(graphics);

                    if (mapTile.ObjectSprite != null)
                    {
                        mapTile.ObjectSprite.Draw(graphics);
                    }
                }
            }

            //And draw the map and any objects
            foreach (MapTile mapTile in Map)
            {
                if (mapTile != null)
                {
                    mapTile.Sprite.Draw(graphics);
                }
                if (mapTile.ObjectSprite != null)
                {
                    mapTile.ObjectSprite.Draw(graphics);
                }
            }




        }
    }
}
