using Astrid.Framework;

namespace Astrid.Maps
{
    public class OrthogonalTileMapTileSet
    {
        public OrthogonalTileMapTileSet(string name, Texture texture, int tileWidth, int tileHeight)
        {
            Name = name;
            Texture = texture;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            var columns = Texture.Width / TileWidth;
            var rows = Texture.Height / TileHeight;
            _textureRegions = new TextureRegion[columns * rows];

            var index = 0;
            var yOffset = 0;

            for (var y = 0; y < rows; y++)
            {
                var xOffset = 0;

                for (var x = 0; x < columns; x++)
                {
                    _textureRegions[index] = new TextureRegion(name, texture, xOffset, yOffset, TileWidth, TileHeight);
                    index++;
                    xOffset += TileWidth;
                }

                yOffset += TileHeight;
            }
        }
        
        public string Name { get; private set; }
        public Texture Texture { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        private readonly TextureRegion[] _textureRegions;

        public TextureRegion GetTextureRegion(int id)
        {
            var index = id - 1;

            if(index >= 0 && index < _textureRegions.Length)
                return _textureRegions[index];

            return null;
        }
    }
}