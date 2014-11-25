using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;

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

        public MapTile[,] Map = new MapTile[MapSizeX, MapSizeY];
        private Rectangle _areaRectangle = new Rectangle(AreaOffsetX, AreaOffsetY, MapSizeX * Tile.TileSizeX, MapSizeY * Tile.TileSizeY);

        public string Name;
        public string NorthArea;
        public string EastArea;
        public string SouthArea;
        public string WestArea;

        public Area(StreamReader stream, Dictionary<string, Tile> tiles)
        {
            string line;

            //1st line is the name
            Name = stream.ReadLine().ToLower();

            //next 4 lines are which areas you go for N,E,S,W
            NorthArea = stream.ReadLine().ToLower();
            EastArea = stream.ReadLine().ToLower();
            SouthArea = stream.ReadLine().ToLower();
            WestArea = stream.ReadLine().ToLower();

            //Read in 8 lines of 8 characters each. Look up the tile and make the
            //matching sprite
            for (int j = 0; j < MapSizeY; j++)
            {
                //Get a line of map characters
                line = stream.ReadLine();

                for (int i = 0; i < MapSizeX; i++)
                {
                    MapTile mapTile = new MapTile();
                    Map[i, j] = mapTile;
                    mapTile.Tile = tiles[line[i].ToString()];
                    mapTile.SetSprite(i, j);
                }
            }

            //Read game objects until the blank line
            while (!stream.EndOfStream && (line = stream.ReadLine().Trim()) != "")
            {
                //Each line is an x,y coordinate and a tile shortcut
                //Look up the tile and construct the sprite
                string[] elements = line.Split(',');
                int x = Convert.ToInt32(elements[0]);
                int y = Convert.ToInt32(elements[1]);
                MapTile mapTile = Map[x, y];
                mapTile.ObjectTile = tiles[elements[2]];
                mapTile.SetObjectSprite(x, y);

                if (mapTile.ObjectTile.IsTransparent)
                {
                    mapTile.ObjectSprite.ColorKey = Color.FromArgb(75, 75, 75);
                }
            }

        }

        public override void Update(double gameTime, double elapsedTime)
        {
            //Update all the map tiles and any objects
            foreach (MapTile mapTile in Map)
            {
                mapTile.Sprite.Update(gameTime, elapsedTime);
                if (mapTile.ObjectSprite != null)
                {
                    if (mapTile.ObjectSprite.NumberOfFrames > 1)
                    {
                        mapTile.ObjectSprite.CurrentFrame = (int)((gameTime * 8.0) % (double)mapTile.ObjectSprite.NumberOfFrames);
                    }
                    mapTile.ObjectSprite.Update(gameTime, elapsedTime);
                }
            }

        }

        public override void Draw(Graphics graphics)
        {
            //And draw the map and any objects
            foreach (MapTile mapTile in Map)
            {
                mapTile.Sprite.Draw(graphics);
                if (mapTile.ObjectSprite != null)
                {
                    mapTile.ObjectSprite.Draw(graphics);
                }
            }

        }
    }
}
