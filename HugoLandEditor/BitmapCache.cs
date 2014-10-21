using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace HugoLandEditeur
{
    /// <summary>
    /// Provides a cache of bitmaps. Will return the existing reference if it exists or load a new one
    /// </summary>
    public class BitmapCache
    {
        private static Dictionary<string, Bitmap> _bitmaps = new Dictionary<string, Bitmap>();

        public Bitmap this[string filename]
        {
            get
            {
                //If this bitmap is not in the cache then load it
                if (!_bitmaps.ContainsKey(filename))
                    _bitmaps.Add(filename, new Bitmap(filename));
                return _bitmaps[filename];
            }
        }
    }
}
