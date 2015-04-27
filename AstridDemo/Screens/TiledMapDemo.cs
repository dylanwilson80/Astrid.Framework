using Astrid;
using Astrid.Animations;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Maps;

namespace AstridDemo.Screens
{
    public class TiledMapDemo : Screen, IKeyInputListener
    {
        public TiledMapDemo(IScreenContext game) 
            : base(game)
        {
        }

        private TiledMapRenderer _tiledMapRenderer;
        private Sprite _blob;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        
        public override void Show()
        {
            _tiledMap = AssetManager.Load<TiledMap>("tiled-map.json");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, AssetManager, _tiledMap, Game.Camera);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var texture = AssetManager.Load<Texture>("blob.png");
            _blob = new Sprite(texture)
            {
                Position = new Vector2(45, 135)
            };

            RegisterInputProcessors();

            base.Show();
        }

        private void RegisterInputProcessors()
        {
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.Left));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.Right));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.Up));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.Down));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.W));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.A));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.S));
            InputDevice.Processors.Add(new KeyInputProcessor(this, Keys.D));
        }

        public override void Render(float deltaTime)
        {
            base.Render(deltaTime);

            _tiledMapRenderer.Draw();

            _spriteBatch.Begin(Game.Camera.GetViewMatrix());
            _blob.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        private Vector2 GetKeyDirection(Keys key)
        {
            if (key == Keys.Left || key == Keys.A)
                return new Vector2(-1, 0);

            if (key == Keys.Right || key == Keys.D)
                return new Vector2(1, 0);

            if (key == Keys.Up || key == Keys.W)
                return new Vector2(0, -1);

            if (key == Keys.Down || key == Keys.S)
                return new Vector2(0, 1);

            return Vector2.Zero;
        }

        private bool _lockMovement;

        public bool OnKeyDown(Keys key)
        {
            var direction = GetKeyDirection(key);

            if (direction != Vector2.Zero)
            {
                if (_lockMovement)
                    return false;

                _lockMovement = true;
                var tileSpaces = GetTileSpaces(direction);
                var distance = new Vector2(_tiledMap.TileWidth, _tiledMap.TileHeight) * tileSpaces;
                var newPosition = _blob.Position + direction * distance;
                
                Animations.CreateSequence(_blob)
                    .MoveTo(newPosition, new TransitionParameters(0.1f * tileSpaces, EasingFunctions.CubicEaseIn))
                    .Execute(() => _lockMovement = false)
                    .Play();
                return true;
            }

            return false;
        }

        private int GetTileSpaces(Vector2 direction)
        {
            return 1;
        }

        public bool OnKeyUp(Keys key)
        {
            return false;
        }
    }
}