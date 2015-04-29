using System.Collections.Generic;
using Astrid.Core;

namespace Astrid.Maps
{
    public class TiledMap : IMovable, IAsset
    {
        private readonly TiledMapData _data;
        private readonly SpriteBatch _spriteBatch;
        private readonly Dictionary<int, TextureRegion> _textureRegions;

        public TiledMap(AssetManager assetManager, GraphicsDevice graphicsDevice, string name, TiledMapData data)
        {
            Name = name;
            _data = data;

            _spriteBatch = new SpriteBatch(graphicsDevice);
            _textureRegions = new Dictionary<int, TextureRegion>();

            foreach (var tileSet in _data.TileSets)
            {
                var tileId = tileSet.FirstGid;
                var texture = assetManager.Load<Texture>(tileSet.Image);

                for (var y = 0; y < tileSet.ImageHeight; y += tileSet.TileHeight)
                {
                    for (var x = 0; x < tileSet.ImageWidth; x += tileSet.TileWidth)
                    {
                        var regionName = tileId.ToString();
                        var tileRegion = new TextureRegion(regionName, texture, x, y, tileSet.TileWidth, tileSet.TileHeight);
                        _textureRegions.Add(tileId, tileRegion);
                        tileId++;
                    }
                }
            }

        }

        public string Name { get; private set; }
        public int TileWidth { get { return _data.TileWidth; } }
        public int TileHeight { get { return _data.TileHeight; } }
        public int Width { get { return _data.Width; } }
        public int Height { get { return _data.Height; } }
        public Vector2 Position { get; set; }

        public void Draw(Camera camera)
        {
            _spriteBatch.Begin(camera.GetViewMatrix());

            foreach (var layer in _data.Layers)
            {
                var tileIndex = 0;

                for (var y = 0; y < layer.Height; y++)
                {
                    for (var x = 0; x < layer.Width; x++)
                    {
                        var tileId = layer.Data[tileIndex];

                        if (tileId != 0)
                        {
                            var region = _textureRegions[tileId];
                            var tileOffset = new Vector2(x * _data.TileWidth, y * _data.TileHeight);
                            var tilePosition = Position + tileOffset;
                            _spriteBatch.Draw(region, tilePosition);
                        }

                        tileIndex++;
                    }
                }
            }

            _spriteBatch.End();
        }

        public ITileInfo GetTileAt(int layerIndex, int x, int y)
        {
            if (layerIndex >= _data.Layers.Count)
                return null;

            if (x < 0 || x >= _data.Width)
                return null;

            if (y < 0 || y >= _data.Height)
                return null;

            var layer = _data.Layers[layerIndex];
            var id = layer.Data[y * layer.Width + x];
            return new TileInfo(this, id, x, y, layerIndex);
        }

        public ITileInfo GetTileAtPosition(int layerIndex, Vector2 position)
        {
            var offset = position - Position;
            var x = (int)(offset.X / TileWidth);
            var y = (int)(offset.Y / TileHeight);
            return GetTileAt(layerIndex, x, y);
        }
    }
}