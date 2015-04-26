using System.Collections.Generic;
using Astrid;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Maps;

namespace AstridDemo.Screens
{
    public class TiledMapRenderer
    {
        private readonly TiledMap _map;
        private readonly Camera _camera;
        private readonly SpriteBatch _spriteBatch;
        private readonly Dictionary<int, TextureRegion> _textureRegions;

        public TiledMapRenderer(GraphicsDevice graphicsDevice, AssetManager assetManager, TiledMap map, Camera camera)
        {
            _map = map;
            _camera = camera;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _textureRegions = new Dictionary<int, TextureRegion>();

            foreach (var tileSet in _map.TileSets)
            {
                var tileId = tileSet.FirstGid;
                var texture = assetManager.Load<Texture>(tileSet.Image);

                for(var x = 0; x < tileSet.ImageWidth; x += tileSet.TileWidth)
                {
                    for(var y = 0; y < tileSet.ImageHeight; y += tileSet.TileHeight)
                    {
                        var regionName = tileId.ToString();
                        var tileRegion = new TextureRegion(regionName, texture, x, y, tileSet.TileWidth, tileSet.TileHeight);
                        _textureRegions.Add(tileId, tileRegion);
                        tileId++;
                    }
                }
            }
        }

        public void Draw()
        {
            _spriteBatch.Begin(_camera.GetViewMatrix());

            foreach (var layer in _map.Layers)
            {
                var tileIndex = 0;

                for(var x = 0; x < layer.Width; x++)
                {
                    for(var y = 0; y < layer.Height; y++)
                    {
                        var tileId = layer.Data[tileIndex];
                        var region = _textureRegions[tileId];
                        var position = new Vector2(x*_map.TileWidth, y*_map.TileHeight);
                        _spriteBatch.Draw(region, position);
                        tileIndex++;
                    }
                }
            }

            _spriteBatch.End();
        }
    }

    public class TiledMapDemo : Screen
    {
        public TiledMapDemo(IScreenContext game) 
            : base(game)
        {
        }

        private TiledMapRenderer _tiledMapRenderer;

        public override void Show()
        {
            var tiledMap = AssetManager.Load<TiledMap>("tiled-map.json");

            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, AssetManager, tiledMap, Game.Camera);

            base.Show();
        }

        public override void Render(float deltaTime)
        {
            base.Render(deltaTime);

            _tiledMapRenderer.Draw();
        }
    }
}