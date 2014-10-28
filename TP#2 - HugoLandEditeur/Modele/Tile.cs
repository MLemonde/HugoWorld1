using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace HugoLandEditeur
{
    /// <summary>
    /// Description: Cette classe représente un carré dans un écran.
    /// </summary>
    public class Tile
    {
        private static BitmapCache _bitmaps = new BitmapCache();
        public const int TileSizeX = 64;
        public const int TileSizeY = 64;

        public Bitmap Bitmap { get; set; }
        public int X_Image { get; set; }
        public int Y_Image { get; set; }

        public Rectangle Rectangle { get; set; }
        public bool IsTransparent { get; set; }
        public int NumberOfFrames { get; set; }
        public bool IsBlock { get; set; }
        public string Category { get; set; }

        public string Color { get; set; }
        public int Health { get; set; }

        public string Name { get; set; }
        public int IndexTypeObjet { get; set; }
        public TypeTile TypeObjet { get; set; }


        public Tile(string[] tileData)
        {
            try
            {
                Name = tileData[0];
                IndexTypeObjet = Convert.ToInt32(tileData[1]);
                Category = tileData[2].ToLower();
                Bitmap = _bitmaps[tileData[3]];
                NumberOfFrames = Convert.ToInt32(tileData[7]);
                Rectangle = new Rectangle((Convert.ToInt32(tileData[4]) - 1) * TileSizeX, (Convert.ToInt32(tileData[5]) - 1) * TileSizeY,
                                           TileSizeX * NumberOfFrames, TileSizeY);

                X_Image = (Convert.ToInt32(tileData[4]) - 1);
                Y_Image = (Convert.ToInt32(tileData[5]) - 1);

                IsTransparent = (tileData[6].ToLower() == "y");
                IsBlock = ((tileData[8].ToLower()) == "block");

                //Some types of tiles have a color
                if (Category == "door" || Category == "key")
                    Color = tileData[9].ToLower();

                //Some types of tiles have health
                if (Category == "character")
                    Health = Convert.ToInt32(tileData[9]);

                //Définir le type d'objet
                switch (tileData[10])
                {
                    case "Objet":
                        TypeObjet = TypeTile.ObjetMonde;
                        break;
                    case "Monstre":
                        TypeObjet = TypeTile.Monstre;
                        break;
                    case "Item":
                        TypeObjet = TypeTile.Item;
                        break;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                foreach (string s in tileData)
                    Console.WriteLine(s);

            }
        }
    }
}
