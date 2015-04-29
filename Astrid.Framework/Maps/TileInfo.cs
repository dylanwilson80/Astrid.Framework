using Astrid.Core;

namespace Astrid.Maps
{
    public interface ITileInfo
    {
        int Id { get; }
        int XIndex { get; }
        int YIndex { get; }
        int LayerIndex { get; }
        int Width { get; }
        int Height { get; }
        Vector2 Centre { get; }
    }

    internal class TileInfo : ITileInfo
    {
        private readonly TiledMap _parent;

        public TileInfo(TiledMap parent, int id, int xIndex, int yIndex, int layerIndex)
        {
            _parent = parent;
            XIndex = xIndex;
            YIndex = yIndex;
            Id = id;
            LayerIndex = layerIndex;
        }

        public int Id { get; private set; }
        public int XIndex { get; private set; }
        public int YIndex { get; private set; }
        public int LayerIndex { get; private set; }

        public int Width
        {
            get { return _parent.TileWidth; }
        }

        public int Height
        {
            get { return _parent.TileHeight; }
        }

        public Vector2 Centre
        {
            get
            {
                var x = XIndex * Width + Width * 0.5f;
                var y = YIndex * Height + Height * 0.5f;
                return _parent.Position + new Vector2(x, y);
            }
        }
    }
}