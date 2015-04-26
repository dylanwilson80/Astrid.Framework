using System.Collections.Generic;

namespace Astrid.Maps
{
    public class TiledMap : IAsset
    {
        public TiledMap(string name)
        {
            Name = name;
            Layers = new List<TiledMapLayer>();
            TileSets = new List<TiledMapTileSet>();
            Properties = new Dictionary<string, string>();
        }

        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string Orientation { get; set; }
        public int Version { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Dictionary<string, string> Properties { get; set; } 
        public List<TiledMapLayer> Layers { get; set; }
        public List<TiledMapTileSet> TileSets { get; set; } 
    }
}