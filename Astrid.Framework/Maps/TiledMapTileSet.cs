using System.Collections.Generic;

namespace Astrid.Maps
{
    public class TiledMapTileSet
    {
        public TiledMapTileSet()
        {
            Properties = new Dictionary<string, string>();
        }

        public string Name { get; set; }
        public int FirstGid { get; set; }
        public string Image { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int Margin { get; set; }
        public int Spacing { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public Dictionary<string, string> Properties { get; set; } 
    }
}