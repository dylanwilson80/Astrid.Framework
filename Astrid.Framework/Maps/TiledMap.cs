using System.Collections.Generic;
using Astrid.Core;

namespace Astrid.Maps
{
    public class TiledMap : IAsset
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
                            var position = new Vector2(x * _data.TileWidth, y * _data.TileHeight);
                            _spriteBatch.Draw(region, position);
                        }

                        tileIndex++;
                    }
                }
            }

            _spriteBatch.End();
        }

        public int GetTileAt(int layerIndex, int x, int y)
        {
            var layer = _data.Layers[layerIndex];
            return layer.Data[y * layer.Width + x];
        }

        public int GetTileAtPosition(int layerIndex, Vector2 position)
        {
            var x = (int) (position.X / TileWidth);
            var y = (int) (position.Y / TileHeight);
            return GetTileAt(layerIndex, x, y);
        }
    }
}