using Astrid;
using Astrid.Animations;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Gui.Fonts;
using Astrid.Maps;

namespace AstridDemo.Screens
{
    public class TiledMapDemo : Screen, IKeyInputListener
    {
        public TiledMapDemo(IScreenContext game) 
            : base(game)
        {
        }

        private Sprite _blob;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private BitmapFont _font;

        public override void Show()
        {
            _tiledMap = AssetManager.Load<TiledMap>("tiled-map.json");
            _tiledMap.Position = new Vector2(15, 40);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var texture = AssetManager.Load<Texture>("blob.png");
            _blob = new Sprite(texture)
            {
                Position = _tiledMap.GetTileAt(1, 0, 0).Centre
            };

            RegisterInputProcessors();

            _font = AssetManager.Load<BitmapFont>("courier-new-32.fnt");

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

            _tiledMap.Draw(Game.Camera);

            _spriteBatch.Begin(Game.Camera.GetViewMatrix());
            
            for (int y = 0; y < _tiledMap.Height; y++)
            {
                for (int x = 0; x < _tiledMap.Width; x++)
                {
                    var tileInfo = _tiledMap.GetTileAt(1, x, y);
                    var text = tileInfo.Id.ToString();
                    var tx = (int) tileInfo.Centre.X;
                    var ty = (int) tileInfo.Centre.Y;
                    var rectangle = _font.MeasureText(text, tx, ty);
                    var color = new Color(Color.Black, 0.5f);
                    _font.Draw(_spriteBatch, text, tx - rectangle.Width / 2, ty - rectangle.Height / 2, color);
                }
            }

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
                    return true;

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
            var tileSpaces = 0;
            var playerTile = _tiledMap.GetTileAtPosition(1, _blob.Position);
            var tileId = 0;
            var x = playerTile.XIndex;
            var y = playerTile.YIndex;

            while (tileId == 0)
            {
                x += (int)direction.X;
                y += (int)direction.Y;
                var tile = _tiledMap.GetTileAt(1, x, y);
                
                if (tile == null)
                    return tileSpaces;
                
                tileId = tile.Id;
                tileSpaces++;
            }

            return tileSpaces - 1;
        }

        public bool OnKeyUp(Keys key)
        {
            return false;
        }
    }
}