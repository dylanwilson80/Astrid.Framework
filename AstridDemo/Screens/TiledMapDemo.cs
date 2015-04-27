using Astrid;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Maps;

namespace AstridDemo.Screens
{
    public class TiledMapDemo : Screen
    {
        public TiledMapDemo(IScreenContext game) 
            : base(game)
        {
        }

        private TiledMapRenderer _tiledMapRenderer;
        private Sprite _blob;
        private SpriteBatch _spriteBatch;

        public override void Show()
        {
            var tiledMap = AssetManager.Load<TiledMap>("tiled-map.json");

            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, AssetManager, tiledMap, Game.Camera);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var texture = AssetManager.Load<Texture>("blob.png");
            _blob = new Sprite(texture)
            {
                Position = new Vector2(45, 135)
            };

            base.Show();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (InputDevice.IsKeyDown(Keys.Right))
                _blob.Position += new Vector2(deltaTime * 90, 0);

            if (InputDevice.IsKeyDown(Keys.Left))
                _blob.Position += new Vector2(deltaTime * -90, 0);

            if (InputDevice.IsKeyDown(Keys.Up))
                _blob.Position += new Vector2(0, deltaTime * -90);

            if (InputDevice.IsKeyDown(Keys.Down))
                _blob.Position += new Vector2(0, deltaTime * 90);
        }

        public override void Render(float deltaTime)
        {
            base.Render(deltaTime);

            _tiledMapRenderer.Draw();

            _spriteBatch.Begin(Game.Camera.GetViewMatrix());
            _blob.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}