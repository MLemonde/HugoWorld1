using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace HugoWorld
{
    /// <summary>
    /// A MapTile is a reference to the original tile and the sprite that represents it. Note that they have to be different
    /// because each tile can be used for multiple map tiles and each sprite has to have a unique location
    /// Each MapTile can also have an other tile object associated with it which is used for special items and monsters
    /// If its a monster then the current health is also stored per MapTile.
    /// </summary>
    public class MapTile
    {
        public Tile Tile;
        public Sprite Sprite;
        public Sprite ObjectSprite;
        public Tile ObjectTile;
        public int ObjectHealth; //A copy of the health of the tile so we remember how damage monsters are

        public void SetSprite(int x, int y)
        {
            //Update the sprite
            Sprite = new Sprite(null, Area.AreaOffsetX + x * Tile.TileSizeX,
                                      Area.AreaOffsetY + y * Tile.TileSizeY,
                                      Tile.Bitmap, Tile.Rectangle,
                                      Tile.NumberOfFrames);
        }

        public void SetObjectSprite(int x, int y)
        {
            //Update the sprite
            ObjectSprite = new Sprite(null, Area.AreaOffsetX + x * Tile.TileSizeX,
                                      Area.AreaOffsetY + y * Tile.TileSizeY,
                                      ObjectTile.Bitmap, ObjectTile.Rectangle,
                                      ObjectTile.NumberOfFrames);
            if (ObjectTile.IsTransparent)
            {
                ObjectSprite.ColorKey = Color.FromArgb(75, 75, 75);
            }
        }

    }

}
